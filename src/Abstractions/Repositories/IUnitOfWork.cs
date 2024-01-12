using Abstractions.Repositories.Books;
using Abstractions.Repositories.Orders;
using Abstractions.Repositories.Users;

namespace Abstractions.Repositories;

public interface IUnitOfWork
{
    public IBookCategoryRepository BookCategories { get; }
    public IBookImageRepository BookImages { get; }
    public IBookRepository Books { get; }
    public ICustomerRepository Customers { get; }
    public IOrderItemRepository OrderItems { get; }
    public IOrderRepository Orders { get; }
    public IPaymentRepository Payments { get; }
    public IShippingRepository Shippings { get; }

    public void Save();
}
