using Domain.Models.Users;

namespace Domain.Dto.Orders;

#pragma warning disable CS8618
public class ShippingDto
{
    public int Id { get; set; }
    public DateOnly ArrivalDate { get; set; }
    public string? Carrier { get; set; }
    public string OrderId { get; set; }
    public string? TrackingNumber { get; set; }
    public FullAddress ShippingAddress { get; set; }
    public DateTime ShippingDate { get; set; }
}
