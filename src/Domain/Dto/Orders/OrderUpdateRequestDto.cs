using Domain.Models.Users;

namespace Domain.Dto.Orders;

#pragma warning disable CS8618
public class OrderUpdateRequestDto
{
    public int Id { get; set; }
    public int CurrentOrderId { get; set; }
    public OrderDto CurrentOrder { get; set; }
    public string? CustomerNameUpdate { get; set; }
    public FullAddress? ShippingAddressUpdate { get; set; }
    public string? Reason { get; set; }
    public string Status { get; set; }
}
