using Abstractions.Repositories;
using Abstractions.Services.Payment;

using AutoMapper;

using Domain.Dto.Orders;
using Domain.Models.Orders;
using Domain.Models.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MVC.Extensions;
using MVC.ViewComponents;

using Stripe.Checkout;

namespace MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public sealed class OrderController(
    IUnitOfWork unitOfWork,
    IStripeRefundService stripeRefundService,
    IStripeSessionService stripeSessionService,
    IMapper mapper) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStripeRefundService _refundService = stripeRefundService;
    private readonly IStripeSessionService _sessionService = stripeSessionService;
    private readonly IMapper _mapper = mapper;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Details(int orderId)
    {
        Order order = _unitOfWork.Orders.GetById(orderId);

        return View(order);
    }

    [HttpPost]
    [Authorize(Roles = $"{Roles.Admin}, {Roles.Employee}")]
    public IActionResult UpdateOrder(Order order)
    {
        // set customer to null to avoid EF tracking error
        order.Customer = null!;

        _unitOfWork.Orders.Update(order);
        _unitOfWork.Save();

        TempData["Success"] = "Order details updated successfully";

        return RedirectToAction(nameof(Details), new { orderId = order.Id });
    }

    [HttpPost]
    [Authorize(Roles = $"{Roles.Customer}, {Roles.Company}")]
    public IActionResult RequestUpdateOrder(Order requestedUpdate, string? reason)
    {
        Order currentOrder = _unitOfWork.Orders.GetById(requestedUpdate.Id);

        OrderUpdateRequest updateRequest = OrderUpdateRequest.Create(currentOrder, requestedUpdate, reason);

        if (_unitOfWork.OrderUpdateRequests.Exists(currentOrder.Id))
            _unitOfWork.OrderUpdateRequests.Remove(currentOrder.Id);

        // set order to null to avoid EF tracking error
        updateRequest.Order = null!;

        _unitOfWork.OrderUpdateRequests.Add(updateRequest);
        _unitOfWork.Save();

        TempData["Success"] = "Order update request sent successfully";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Roles = $"{Roles.Admin}, {Roles.Employee}")]
    public IActionResult StartProcessing(int orderId)
    {
        _unitOfWork.Orders.UpdateStatus(orderId, OrderStatus.Processing);
        _unitOfWork.Save();

        TempData["Success"] = "Order status updated to Processing";

        return RedirectToAction(nameof(Details), new { orderId });
    }

    [HttpPost]
    [Authorize(Roles = $"{Roles.Admin}, {Roles.Employee}")]
    public IActionResult ShipOrder(Order order)
    {
        // set customer to null to avoid EF tracking error
        order.Customer = null!;

        order.Status = OrderStatus.Shipped;

        if (order.Shipping.ShippingDate == default)
            order.Shipping.ShippingDate = DateTime.Now;

        order.Shipping.ArrivalDate = DateOnly.FromDateTime(DateTime.Now.AddDays(new Random().Next(4, 11)));

        if (order.Payment.Status == PaymentStatus.Delayed)
            order.Payment.DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));

        _unitOfWork.Orders.Update(order);
        _unitOfWork.Save();

        TempData["Success"] = "Order shipped successfully";

        return RedirectToAction(nameof(Details), new { orderId = order.Id });
    }

    [HttpPost]
    [Authorize]
    public IActionResult CancelOrder(int orderId)
    {
        Order order = _unitOfWork.Orders.GetById(orderId);

        if (order.Payment.Status == PaymentStatus.Approved)
        {
            _ = _refundService.CreateRefund(order.Payment.PaymentIntentId!, order.TotalPrice);

            _unitOfWork.Orders.UpdateStatus(order.Id, OrderStatus.Cancelled);
            _unitOfWork.Payments.UpdateStatus(order.PaymentId, PaymentStatus.Refunded);
        }
        else
        {
            _unitOfWork.Orders.UpdateStatus(order.Id, OrderStatus.Cancelled);
            _unitOfWork.Payments.UpdateStatus(order.PaymentId, PaymentStatus.Cancelled);
        }

        _unitOfWork.Save();

        TempData["Success"] = "Order cancelled successfully";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Roles = Roles.Company)]
    public IActionResult Pay(int orderId)
    {
        Order order = _unitOfWork.Orders.GetById(orderId);

        Session session = _sessionService.CreateSession(order.Items, GetStripeSessionSchemas(order));

        _unitOfWork.Payments.UpdateStripePaymentId(order.Payment.Id, session.Id, session.PaymentIntentId);
        _unitOfWork.Save();

        Response.Headers.Append("Location", session.Url);

        return new StatusCodeResult(303);
    }

    [Authorize(Roles = Roles.Company)]
    public IActionResult OrderConfirmation(int orderId)
    {
        Order order = _unitOfWork.Orders.GetById(orderId);

        ClearShoppingCart(order);

        var session = GetSessionById(order.Payment.SessionId!);

        if (session.IsPaid())
            UpdatePaymentStatus(order, session);

        TempData["Success"] = "Order paid successfully";

        return RedirectToAction(nameof(Details), new { orderId });
    }

    #region API calls

    [HttpGet]
    public IActionResult GetAll(string status)
    {
        IEnumerable<Order> orders;

        if (User.IsInRole(Roles.Admin) || User.IsInRole(Roles.Employee))
        {
            orders = GetOrders(status);
        }
        else
        {
            var customerId = this.GetUserId();
            orders = GetOrdersByCustomer(customerId, status);
        }

        var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

        return Json(new { data = orderDtos });
    }

    private IEnumerable<Order> GetOrders(string status)
    {
        return status switch
        {
            "paymentPending" => _unitOfWork.Orders.GetByPaymentStatus(PaymentStatus.Delayed),
            "inProcess" => _unitOfWork.Orders.GetByOrderStatus(OrderStatus.Processing),
            "cancelled" => _unitOfWork.Orders.GetByOrderStatus(OrderStatus.Cancelled),
            "completed" => _unitOfWork.Orders.GetByOrderStatus(OrderStatus.Shipped),
            "approved" => _unitOfWork.Orders.GetByOrderStatus(OrderStatus.Approved),
            _ => _unitOfWork.Orders.GetAll()
        };
    }

    private IEnumerable<Order> GetOrdersByCustomer(string customerId, string status)
    {
        return status switch
        {
            "paymentPending" => _unitOfWork.Orders.GetByPaymentStatus(PaymentStatus.Delayed, customerId),
            "inProcess" => _unitOfWork.Orders.GetByOrderStatus(OrderStatus.Processing, customerId),
            "cancelled" => _unitOfWork.Orders.GetByOrderStatus(OrderStatus.Cancelled, customerId),
            "completed" => _unitOfWork.Orders.GetByOrderStatus(OrderStatus.Shipped, customerId),
            "approved" => _unitOfWork.Orders.GetByOrderStatus(OrderStatus.Approved, customerId),

            _ => _unitOfWork.Orders.GetByCustomer(customerId)
        };
    }

    #endregion

    private static Session GetSessionById(string sessionId)
    {
        var service = new SessionService();
        return service.Get(sessionId);
    }

    private void UpdatePaymentStatus(Order order, Session session)
    {
        _unitOfWork.Payments.UpdateStripePaymentId(order.PaymentId, session.Id, session.PaymentIntentId);
        _unitOfWork.Payments.UpdateStatus(order.PaymentId, PaymentStatus.Approved);
        _unitOfWork.Orders.UpdateStatus(order.Id, OrderStatus.Approved);

        _unitOfWork.Save();
    }

    private void ClearShoppingCart(Order order)
    {
        var bookCarts = _unitOfWork.BookCarts.GetByCustomer(order.CustomerId);

        _unitOfWork.BookCarts.RemoveRange(bookCarts);
        _unitOfWork.Save();

        HttpContext.Session.SetInt32(SessionValues.BookCartsCount, 0);
    }

    private StripeSessionUrlSchemas GetStripeSessionSchemas(Order order)
    {
        string domain = $"{Request.Scheme}://{Request.Host}";

        return new()
        {
            CancelUrl = $"{domain}/Customer/Cart/OrderConfirmation?headerId={order.Id}",
            SuccessUrl = $"{domain}/Customer/Cart/Index"
        };
    }
}
