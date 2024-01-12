using Abstractions.Repositories.Books;

using DataAccess.Data;
using DataAccess.Exceptions;

using Domain.Models.Books;

namespace DataAccess.Repositories.Books;

public class BookCategoryRepository(LitMarketDbContext db)
    : ModelRepository<BookCategory>(db), IBookCategoryRepository
{
    public BookCategory GetByName(string name)
    {
        return GetByFilter(n => n.Name == name).FirstOrDefault()
            ?? throw new ModelNotFoundException<BookCategory>(n => n.Name == name);
    }
}
