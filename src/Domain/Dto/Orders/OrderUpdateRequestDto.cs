using Domain.Dto.Users;

namespace Domain.Dto.Orders;

#pragma warning disable CS8618
public class OrderUpdateRequestDto
{
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public CustomerDto Customer { get; set; }
    public int CurrentOrderId { get; set; }
    public OrderDto CurrentOrder { get; set; }
    public string ShippingId { get; set; }
    public ShippingDto Shipping { get; set; }
    public IEnumerable<OrderItemDto> Items { get; set; }
    public string PaymentId { get; set; }
    public PaymentDto Payment { get; set; }
    public double TotalPrice { get; set; }
    public string Status { get; set; }
}
