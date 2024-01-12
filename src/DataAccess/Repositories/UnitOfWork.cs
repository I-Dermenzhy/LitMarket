using Abstractions.Repositories;
using Abstractions.Repositories.Books;
using Abstractions.Repositories.Carts;
using Abstractions.Repositories.Orders;
using Abstractions.Repositories.Users;

using DataAccess.Data;

namespace DataAccess.Repositories;

public sealed class UnitOfWork(
    LitMarketDbContext db,
    IBookCartRepository bookCartRepository,
    IBookImageRepository bookImageRepository,
    IBookRepository bookRepository,
    ICustomerRepository customerRepository,
    IGenreRepository genreRepository,
    IOrderItemRepository orderItemRepository,
    IOrderRepository orderRepository,
    IPaymentRepository paymentRepository,
    IPriceListRepository priceListRepository,
    IShippingRepository shippingRepository) : IUnitOfWork
{
    private readonly LitMarketDbContext _db = db;

    public IBookCartRepository BookCarts { get; } = bookCartRepository;
    public IBookImageRepository BookImages { get; } = bookImageRepository;
    public IBookRepository Books { get; } = bookRepository;
    public ICustomerRepository Customers { get; } = customerRepository;
    public IGenreRepository Genres { get; } = genreRepository;
    public IOrderItemRepository OrderItems { get; } = orderItemRepository;
    public IOrderRepository Orders { get; } = orderRepository;
    public IPaymentRepository Payments { get; } = paymentRepository;
    public IPriceListRepository PriceLists { get; } = priceListRepository;
    public IShippingRepository Shippings { get; } = shippingRepository;

    public void Save() => _db.SaveChanges();
}
