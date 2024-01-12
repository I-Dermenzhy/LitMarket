using Abstractions.Repositories.Orders;

using DataAccess.Data;

using Domain.Models.Orders;

namespace DataAccess.Repositories.Orders;

public class OrderRepository(LitMarketDbContext db)
    : ModelRepository<Order>(db), IOrderRepository
{
    public IEnumerable<Order> GetByOrderStatus(OrderStatus status, string? customerId = null)
    {
        if (customerId is null)
            return GetByFilter(o => o.Status == status);

        return GetByFilter(o => o.Status == status && o.CustomerId == customerId);
    }

    public IEnumerable<Order> GetByPaymentStatus(PaymentStatus status, string? customerId = null)
    {
        if (customerId is null)
            return GetByFilter(o => o.Payment.Status == status);

        return GetByFilter(o => o.Payment.Status == status && o.CustomerId == customerId);
    }

    public IEnumerable<Order> GetByCustomer(string customerId)
    {
        return GetByFilter(o => o.CustomerId == customerId);
    }

    public void UpdateStatus(int id, OrderStatus status)
    {
        Order order = GetById(id, isTracked: true);
        order.Status = status;
    }
}

