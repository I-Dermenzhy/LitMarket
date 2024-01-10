using Domain.Models.Orders;

namespace Abstractions.Repositories.Orders;

public interface IDeliveryRepository : IModelRepository<Delivery>
{
    public void UpdateDeliveryDetails(int id, string carrier, string trackingNumber);
}
