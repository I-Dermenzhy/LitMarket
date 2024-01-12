using Abstractions.Repositories.Books;

using DataAccess.Data;
using DataAccess.Exceptions;

using Domain.Models.Books;

namespace DataAccess.Repositories.Books;

public class BookCategoryRepository(LitMarketDbContext db)
    : ModelRepository<Genre>(db), IGenreRepository
{
    public Genre GetByName(string name)
    {
        return GetByFilter(n => n.Name == name).FirstOrDefault()
            ?? throw new ModelNotFoundException<Genre>(n => n.Name == name);
    }
}
