using Domain.Models.Orders;

namespace Abstractions.Repositories.Orders;

public interface IOrderUpdateRequestRepository : IModelRepository<OrderUpdateRequest>
{
    public OrderUpdateRequest GetByOrder(int orderId);
}
