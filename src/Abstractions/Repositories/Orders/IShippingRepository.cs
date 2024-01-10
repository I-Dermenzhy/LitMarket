using Domain.Models.Orders;

namespace Abstractions.Repositories.Orders;

public interface IShippingRepository : IModelRepository<Shipping>
{
    public void UpdateShipmentDetails(int id, string carrier, string trackingNumber);
}
