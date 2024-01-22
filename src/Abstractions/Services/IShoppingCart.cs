using Domain.Models.Carts;
using Domain.Models.Orders;

namespace Abstractions.Services;

public interface IShoppingCart
{
    public IEnumerable<BookCart> BookCarts { get; set; }
    public Order Order { get; set; }
}
