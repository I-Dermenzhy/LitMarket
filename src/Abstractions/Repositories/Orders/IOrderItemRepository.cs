using Domain.Models.Orders;

namespace Abstractions.Repositories.Orders;

public interface IOrderItemRepository : IModelRepository<OrderItem>
{
    public IEnumerable<OrderItem> GetByOrder(int orderId);
}
