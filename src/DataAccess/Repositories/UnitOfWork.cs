using Abstractions.Repositories;
using Abstractions.Repositories.Books;
using Abstractions.Repositories.Orders;
using Abstractions.Repositories.Users;

using DataAccess.Data;

namespace DataAccess.Repositories;

public sealed class UnitOfWork(
    LitMarketDbContext db,
    IBookCategoryRepository bookCategoryRepository,
    IBookImageRepository bookImageRepository,
    IBookRepository bookRepository,
    ICustomerRepository customerRepository,
    IOrderItemRepository orderItemRepository,
    IOrderRepository orderRepository,
    IPaymentRepository paymentRepository,
    IPriceListRepository priceListRepository,
    IShippingRepository shippingRepository) : IUnitOfWork
{
    private readonly LitMarketDbContext _db = db;

    public IBookCategoryRepository BookCategories { get; } = bookCategoryRepository;
    public IBookImageRepository BookImages { get; } = bookImageRepository;
    public IBookRepository Books { get; } = bookRepository;
    public ICustomerRepository Customers { get; } = customerRepository;
    public IOrderItemRepository OrderItems { get; } = orderItemRepository;
    public IOrderRepository Orders { get; } = orderRepository;
    public IPaymentRepository Payments { get; } = paymentRepository;
    public IPriceListRepository PriceLists { get; } = priceListRepository;
    public IShippingRepository Shippings { get; } = shippingRepository;

    public void Save() => _db.SaveChanges();
}
