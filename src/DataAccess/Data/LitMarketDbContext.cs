using Domain.Models.Books;
using Domain.Models.Orders;
using Domain.Models.Products;
using Domain.Models.Users;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;

public class LitMarketDbContext(DbContextOptions<LitMarketDbContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookImage> BookImages => Set<BookImage>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Shipping> Shippings => Set<Shipping>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LitMarketDbContext).Assembly);

        base.OnModelCreating(modelBuilder);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        #region BookCategories
        var action = new Genre { Id = 1, Name = "Action", DisplayOrder = 1 };
        var adventure = new Genre { Id = 2, Name = "Adventure", DisplayOrder = 2 };
        var dystopia = new Genre { Id = 4, Name = "Dystopian Fiction", DisplayOrder = 3 };
        var drama = new Genre { Id = 5, Name = "Drama", DisplayOrder = 4 };
        var fantasy = new Genre { Id = 7, Name = "Fantasy", DisplayOrder = 5 };
        var history = new Genre { Id = 3, Name = "History", DisplayOrder = 6 };
        var romance = new Genre { Id = 6, Name = "Romance", DisplayOrder = 7 };

        modelBuilder.Entity<Genre>().HasData(
            action, adventure, history, dystopia, drama, romance, fantasy
        );
        #endregion

        #region PriceLists
        var fortuneOfTimePriceList = new PriceList
        {
            Id = 1,
            ListPrice = 99,
            Price = 90,
            Price50 = 85,
            Price100 = 80
        };

        var darkSkiesPriceList = new PriceList
        {
            Id = 2,
            ListPrice = 40,
            Price = 30,
            Price50 = 25,
            Price100 = 20
        };

        var vanishInTheSunsetPriceList = new PriceList
        {
            Id = 3,
            ListPrice = 55,
            Price = 50,
            Price50 = 40,
            Price100 = 35
        };

        var cottonCandyPriceList = new PriceList
        {
            Id = 4,
            ListPrice = 70,
            Price = 65,
            Price50 = 60,
            Price100 = 55
        };

        var rockInTheOceanPriceList = new PriceList
        {
            Id = 5,
            ListPrice = 30,
            Price = 27,
            Price50 = 25,
            Price100 = 20
        };

        var leavesAndWondersPriceList = new PriceList
        {
            Id = 6,
            ListPrice = 25,
            Price = 23,
            Price50 = 22,
            Price100 = 20
        };

        modelBuilder.Entity<PriceList>().HasData(
            fortuneOfTimePriceList, darkSkiesPriceList, vanishInTheSunsetPriceList,
            cottonCandyPriceList, rockInTheOceanPriceList, leavesAndWondersPriceList
        );
        #endregion

        #region Books
        const string description = @"Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. 
            Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ";

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "Fortune of Time",
                Author = "Billy Spark",
                GenreId = history.Id,
                Description = description,
                ISBN = "SWD9999001",
                PriceListId = fortuneOfTimePriceList.Id
            },
            new Book
            {
                Id = 2,
                Title = "Dark Skies",
                Author = "Nancy Hoover",
                GenreId = dystopia.Id,
                Description = description,
                ISBN = "CAW777777701",
                PriceListId = darkSkiesPriceList.Id
            },
            new Book
            {
                Id = 3,
                Title = "Vanish in the Sunset",
                Author = "Julian Button",
                GenreId = drama.Id,
                Description = description,
                ISBN = "RITO5555501",
                PriceListId = vanishInTheSunsetPriceList.Id
            },
            new Book
            {
                Id = 4,
                Title = "Cotton Candy",
                Author = "Abby Muscles",
                GenreId = romance.Id,
                Description = description,
                ISBN = "WS3333333301",
                PriceListId = cottonCandyPriceList.Id
            },
            new Book
            {
                Id = 5,
                Title = "Rock in the Ocean",
                Author = "Ron Parker",
                GenreId = adventure.Id,
                Description = description,
                ISBN = "SOTJ1111111101",
                PriceListId = rockInTheOceanPriceList.Id
            },
            new Book
            {
                Id = 6,
                Title = "Leaves and Wonders",
                Author = "Laura Phantom",
                GenreId = action.Id,
                Description = description,
                ISBN = "FOT000000001",
                PriceListId = leavesAndWondersPriceList.Id
            }
        );
        #endregion
    }
}
