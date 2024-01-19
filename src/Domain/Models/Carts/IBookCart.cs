using Domain.Models.Books;

namespace Domain.Models.Carts;

public interface IBookCart : IModel
{
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int Count { get; set; }
    public double GetTotalPrice();
}
