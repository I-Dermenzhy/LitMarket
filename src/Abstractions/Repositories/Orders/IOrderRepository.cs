using Domain.Models.Orders;

namespace Abstractions.Repositories.Orders;

public interface IOrderRepository : IModelRepository<Order>
{
    public IEnumerable<Order> GetByOrderStatus(string status, string? userId = null);
    public IEnumerable<Order> GetByPaymentStatus(string status, string? userId = null);
    public IEnumerable<Order> GetByUser(string userId);

    public void UpdateStatus(int id, string orderStatus);
}
