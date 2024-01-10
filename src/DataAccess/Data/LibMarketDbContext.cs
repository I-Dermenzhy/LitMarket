using Domain.Models.Books;
using Domain.Models.Orders;
using Domain.Models.Products;
using Domain.Models.Users;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;

public class LibMarketDbContext(DbContextOptions<LibMarketDbContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookCategory> BookCategories => Set<BookCategory>();
    public DbSet<BookImage> BookImages => Set<BookImage>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Shipping> Shippings => Set<Shipping>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibMarketDbContext).Assembly);

        base.OnModelCreating(modelBuilder);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
    }
}
