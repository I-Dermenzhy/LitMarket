using Abstractions.Repositories;
using Abstractions.Repositories.Books;
using Abstractions.Repositories.Carts;
using Abstractions.Repositories.Orders;
using Abstractions.Repositories.Users;

using DataAccess.Repositories;
using DataAccess.Repositories.Books;
using DataAccess.Repositories.Carts;
using DataAccess.Repositories.Orders;
using DataAccess.Repositories.Users;

using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddLitMarketRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBookCartRepository, BookCartRepository>();
        services.AddScoped<IBookImageRepository, BookImageRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IGenreRepository, BookCategoryRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPriceListRepository, PriceListRepository>();
        services.AddScoped<IShippingRepository, ShippingRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
