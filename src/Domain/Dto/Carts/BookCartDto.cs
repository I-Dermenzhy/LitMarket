namespace Domain.Dto.Carts;

#pragma warning disable CS8618
public class BookCartDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int Count { get; set; }
    public string CustomerId { get; set; }
    public double PricePerBook { get; set; }
    public double TotalPrice { get; set; }
}
