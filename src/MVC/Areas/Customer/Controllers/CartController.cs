using Abstractions.Repositories;
using Abstractions.Services.Payment;

using Domain.Models.Carts;
using Domain.Models.Orders;
using Domain.Models.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MVC.Areas.Customer.ViewModels;
using MVC.Extensions;
using MVC.ViewComponents;

using Stripe.Checkout;

namespace MVC.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize(Roles = $"{Roles.Customer},{Roles.Company}")]
public sealed class CartController(
    IUnitOfWork unitOfWork,
    IStripeRefundService stripeRefundService,
    IStripeSessionService stripeSessionService) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStripeRefundService _stripeRefundService = stripeRefundService;
    private readonly IStripeSessionService _stripeSessionService = stripeSessionService;

    [BindProperty]
    private ShoppingCartVM ShoppingCartVM { get; set; } = new();

    public IActionResult Index()
    {
        ShoppingCartVM.BookCarts = GetBookCarts();
        ShoppingCartVM.Order = new()
        {
            TotalPrice = ShoppingCartVM.BookCarts.Sum(c => c.GetTotalPrice()),
            /*Shipping = new(),
			Payment = new()*/
        };

        return View(ShoppingCartVM);
    }

    #region Modify Cart

    public IActionResult Plus(int bookCartId)
    {
        var bookCart = _unitOfWork.BookCarts.GetById(bookCartId);

        bookCart.Count += 1;

        _unitOfWork.BookCarts.Update(bookCart);
        _unitOfWork.Save();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Minus(int bookCartId)
    {
        var bookCart = _unitOfWork.BookCarts.GetById(bookCartId);

        bookCart.Count -= 1;

        if (bookCart.Count == 0)
            return Remove(bookCartId);

        _unitOfWork.BookCarts.Update(bookCart);
        _unitOfWork.Save();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Remove(int bookCartId)
    {
        var bookCart = _unitOfWork.BookCarts.GetById(bookCartId);

        _unitOfWork.BookCarts.Remove(bookCart);
        _unitOfWork.Save();

        UpdateCartCount(bookCart.CustomerId);

        return RedirectToAction(nameof(Index));
    }

    #endregion

    public IActionResult Summary()
    {
        ShoppingCartVM.BookCarts = GetBookCarts();
        ShoppingCartVM.Order = GetPopulatedOrder();

        return View(ShoppingCartVM);
    }

    [HttpPost]
    [ActionName("Summary")]
    public IActionResult SummaryPost(ShoppingCartVM shoppingCartVM)
    {
        ShoppingCartVM.BookCarts = GetBookCarts();
        ShoppingCartVM.Order = GetPopulatedOrderForPlacing(shoppingCartVM.Order.Shipping);

        AddOrderToDb();
        AddOrderItemsToDb();

        if (!this.IsCompany())
        {
            ProcessPaymentSession();

            return new StatusCodeResult(303);
        }

        return RedirectToAction(nameof(OrderConfirmation), new { orderId = ShoppingCartVM.Order.Id });
    }

    public IActionResult OrderConfirmation(int orderId)
    {
        ShoppingCartVM.Order = _unitOfWork.Orders.GetById(orderId);

        ClearShoppingCart();

        if (!this.IsCompany())
        {
            var session = GetSessionById(ShoppingCartVM.Order.Payment.SessionId!);

            if (session.IsPaid())
                UpdatePaymentStatus(ShoppingCartVM.Order, session);
        }

        TempData["Success"] = "Order placed successfully";

        return RedirectToAction(nameof(Index), "Home");
    }

    private static Session GetSessionById(string sessionId)
    {
        var service = new SessionService();
        return service.Get(sessionId);
    }

    private void ClearShoppingCart()
    {
        var order = _unitOfWork.Orders.GetById(ShoppingCartVM.Order.Id);
        var bookCarts = _unitOfWork.BookCarts.GetByCustomer(order.CustomerId);

        _unitOfWork.BookCarts.RemoveRange(bookCarts);
        _unitOfWork.Save();

        HttpContext.Session.SetInt32(SessionValues.BookCartsCount, 0);
    }

    private void UpdatePaymentStatus(Order order, Session session)
    {
        _unitOfWork.Payments.UpdateStripePaymentId(order.PaymentId, session.Id, session.PaymentIntentId);
        _unitOfWork.Payments.UpdateStatus(order.PaymentId, PaymentStatus.Approved);
        _unitOfWork.Orders.UpdateStatus(order.Id, OrderStatus.Approved);

        _unitOfWork.Save();
    }

    private IEnumerable<BookCart> GetBookCarts()
    {
        var userId = this.GetUserId();
        return _unitOfWork.BookCarts.GetByCustomer(userId);
    }

    private Order GetPopulatedOrder()
    {
        return new()
        {
            Customer = _unitOfWork.Customers.GetById(this.GetUserId()),
            TotalPrice = ShoppingCartVM.BookCarts.Sum(c => c.GetTotalPrice())
        };
    }

    private Order GetPopulatedOrderForPlacing(Shipping shipping)
    {
        Order order = GetPopulatedOrder();

        order.OrderDate = DateTime.Now;
        order.CustomerId = order.Customer.Id;

        order.Shipping = shipping;
        order.Payment = new();

        if (this.IsCompany())
        {
            order.Status = OrderStatus.Approved;
            order.Payment.Status = PaymentStatus.Delayed;
        }
        else
        {
            order.Status = OrderStatus.Pending;
            order.Payment.Status = PaymentStatus.Pending;
        }

        return order;
    }

    private void ProcessPaymentSession()
    {
        Session session = CreateStripeSession();

        _unitOfWork.Payments.UpdateStripePaymentId(ShoppingCartVM.Order.Payment.Id, session.Id, session.PaymentIntentId);
        _unitOfWork.Save();

        Response.Headers.Append("Location", session.Url);
    }

    private Session CreateStripeSession()
    {
        string domain = $"{Request.Scheme}://{Request.Host}";

        StripeSessionUrlSchemas schemas = new()
        {
            CancelUrl = $"{domain}/Customer/Cart/Index",
            SuccessUrl = $"{domain}/Customer/Cart/OrderConfirmation?orderId={ShoppingCartVM.Order.Id}"
        };

        return _stripeSessionService.CreateSession(ShoppingCartVM.BookCarts, schemas);
    }

    private void AddOrderToDb()
    {
        // Set the customer to null to avoid EF tracking error.
        ShoppingCartVM.Order.Customer = null!;

        _unitOfWork.Orders.Add(ShoppingCartVM.Order);
        _unitOfWork.Payments.Add(ShoppingCartVM.Order.Payment);
        _unitOfWork.Shippings.Add(ShoppingCartVM.Order.Shipping);
        _unitOfWork.Save();
    }

    private void AddOrderItemsToDb()
    {
        foreach (var bookCart in ShoppingCartVM.BookCarts)
        {
            OrderItem orderItem = new()
            {
                BookId = bookCart.BookId,
                OrderId = ShoppingCartVM.Order.Id,
                Price = bookCart.Book.PriceList.Price,
                Count = bookCart.Count
            };

            _unitOfWork.OrderItems.Add(orderItem);
        }

        _unitOfWork.Save();
    }

    private void UpdateCartCount(string customerId)
    {
        int bookCartsCount = _unitOfWork.BookCarts.GetByCustomer(customerId).Count();
        HttpContext.Session.SetInt32(SessionValues.BookCartsCount, bookCartsCount);
    }
}
