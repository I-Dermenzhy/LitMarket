using Abstractions.Repositories.Orders;

using DataAccess.Data;
using DataAccess.Exceptions;

using Domain.Models.Orders;

namespace DataAccess.Repositories.Orders;

public class OrderUpdateRequestRepository(LitMarketDbContext db)
        : ModelRepository<OrderUpdateRequest>(db), IOrderUpdateRequestRepository
{
    public OrderUpdateRequest GetByCurrentOrder(int currentOrderId)
    {
        return GetByFilter(our => our.CurrentOrderId == currentOrderId).FirstOrDefault()
            ?? throw new ModelNotFoundException<OrderUpdateRequest>(our => our.CurrentOrderId == currentOrderId);
    }
}
