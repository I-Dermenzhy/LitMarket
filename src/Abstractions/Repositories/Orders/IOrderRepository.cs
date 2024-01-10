using Domain.Models.Orders;

namespace Abstractions.Repositories.Orders;

public interface IOrderRepository : IModelRepository<Order>
{
    public IEnumerable<Order> GetByOrderStatus(OrderStatus status, string? customerId = null);
    public IEnumerable<Order> GetByPaymentStatus(PaymentStatus status, string? customerId = null);
    public IEnumerable<Order> GetByCustomer(string customerId);

    public void UpdateStatus(int id, OrderStatus orderStatus);
}
