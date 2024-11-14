using Abstractions.Services;

using Domain.Models.Carts;
using Domain.Models.Orders;

namespace MVC.Areas.Customer.ViewModels;

#pragma warning disable CS8618
public class ShoppingCartVM : IShoppingCart
{
    public IEnumerable<BookCart> BookCarts { get; set; }
    public Order Order { get; set; }
}
