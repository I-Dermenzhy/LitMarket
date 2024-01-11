namespace Domain.Dto.Orders;

public class OrderItemDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int Count { get; set; }
    public int OrderId { get; set; }
    public double Price { get; set; }
    public double TotalPrice => Price * Count;
}