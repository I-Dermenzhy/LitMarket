using Abstractions.Repositories.Books;

using DataAccess.Data;

using Domain.Models.Products;

namespace DataAccess.Repositories.Books;

public class PriceListRepository(LitMarketDbContext db)
    : ModelRepository<PriceList>(db), IPriceListRepository
{
}
