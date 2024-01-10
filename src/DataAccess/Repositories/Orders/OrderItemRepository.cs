using Abstractions.Repositories.Orders;

using DataAccess.Data;

using Domain.Models.Orders;

namespace DataAccess.Repositories.Orders;

public class OrderItemRepository(LibMarketDbContext db)
        : ModelRepository<OrderItem>(db), IOrderItemRepository
{
    public IEnumerable<OrderItem> GetByOrder(int orderId)
    {
        return GetByFilter(oi => oi.OrderId == orderId);
    }
}
