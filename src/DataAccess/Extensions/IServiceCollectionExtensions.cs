using Abstractions.Repositories;
using Abstractions.Repositories.Books;
using Abstractions.Repositories.Orders;
using Abstractions.Repositories.Users;

using DataAccess.Repositories;
using DataAccess.Repositories.Books;
using DataAccess.Repositories.Orders;
using DataAccess.Repositories.Users;

using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddLitMarketRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
        services.AddScoped<IBookImageRepository, BookImageRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IShippingRepository, ShippingRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
