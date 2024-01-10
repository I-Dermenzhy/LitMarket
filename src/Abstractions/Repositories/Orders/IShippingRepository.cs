using Domain.Models.Orders;

namespace Abstractions.Repositories.Orders;

public interface IShippingRepository : IModelRepository<Shipping>
{
    public void UpdateShippingDetails(int id, string carrier, string trackingNumber, DateOnly arrivalDate);
}
