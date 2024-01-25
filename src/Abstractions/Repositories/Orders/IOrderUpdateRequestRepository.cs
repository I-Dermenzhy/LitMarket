using Domain.Models.Orders;

namespace Abstractions.Repositories.Orders;

public interface IOrderUpdateRequestRepository : IModelRepository<OrderUpdateRequest>
{
    public bool Exists(int orderId);
    public OrderUpdateRequest GetByOrder(int orderId);
}
