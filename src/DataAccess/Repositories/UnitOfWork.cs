using Abstractions.Repositories.Books;
using Abstractions.Repositories.Orders;
using Abstractions.Repositories.Users;

using DataAccess.Data;

namespace DataAccess.Repositories;

public sealed class UnitOfWork(
    LibMarketDbContext db,
    IBookCategoryRepository bookCategoryRepository,
    IBookImageRepository bookImageRepository,
    IBookRepository bookRepository,
    ICustomerRepository customerRepository,
    IOrderItemRepository orderItemRepository,
    IOrderRepository orderRepository,
    IPaymentRepository paymentRepository,
    IPriceListRepository priceListRepository,
    IShippingRepository shippingRepository)
{
    private readonly LibMarketDbContext _db = db;

    public IBookCategoryRepository BookCategoryRepository { get; } = bookCategoryRepository;
    public IBookImageRepository BookImageRepository { get; } = bookImageRepository;
    public IBookRepository BookRepository { get; } = bookRepository;
    public ICustomerRepository CustomerRepository { get; } = customerRepository;
    public IOrderItemRepository OrderItemRepository { get; } = orderItemRepository;
    public IOrderRepository OrderRepository { get; } = orderRepository;
    public IPaymentRepository PaymentRepository { get; } = paymentRepository;
    public IPriceListRepository PriceListRepository { get; } = priceListRepository;
    public IShippingRepository ShippingRepository { get; } = shippingRepository;

    public void Save() => _db.SaveChanges();
}
