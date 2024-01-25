using Abstractions.Repositories.Orders;

using DataAccess.Data;
using DataAccess.Exceptions;

using Domain.Models.Orders;

namespace DataAccess.Repositories.Orders;

public class OrderUpdateRequestRepository(LitMarketDbContext db)
        : ModelRepository<OrderUpdateRequest>(db), IOrderUpdateRequestRepository
{
    public OrderUpdateRequest GetByOrder(int orderId)
    {
        return GetByFilter(our => our.OrderId == orderId).FirstOrDefault()
            ?? throw new ModelNotFoundException<OrderUpdateRequest>(our => our.OrderId == orderId);
    }

    public bool Exists(int orderId) => GetByFilter(our => our.OrderId == orderId).Any();
}
