using Abstractions.Repositories.Books;

using DataAccess.Data;

using Domain.Models.Products;

namespace DataAccess.Repositories.Books;

public class PriceListRepository(LibMarketDbContext db)
    : ModelRepository<PriceList>(db), IPriceListRepository
{
}
