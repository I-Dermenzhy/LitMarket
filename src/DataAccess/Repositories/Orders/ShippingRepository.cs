using Abstractions.Repositories.Orders;

using DataAccess.Data;

using Domain.Models.Orders;

namespace DataAccess.Repositories.Orders;

public class ShippingRepository(LitMarketDbContext db)
    : ModelRepository<Shipping>(db), IShippingRepository
{
    public void UpdateShippingDetails(int id, string carrier, string trackingNumber, DateOnly arrivalDate)
    {
        Shipping shipping = GetById(id, isTracked: true);

        shipping.Carrier = carrier;
        shipping.TrackingNumber = trackingNumber;
        shipping.ShippingDate = DateTime.Now;
        shipping.ArrivalDate = arrivalDate;
    }
}
