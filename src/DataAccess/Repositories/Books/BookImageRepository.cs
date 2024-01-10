using Abstractions.Repositories.Books;

using DataAccess.Data;
using DataAccess.Exceptions;

using Domain.Models.Products;

namespace DataAccess.Repositories.Books;

public class BookImageRepository(LibMarketDbContext db)
    : ModelRepository<BookImage>(db), IBookImageRepository
{
    public IEnumerable<BookImage> GetByBook(int bookId)
    {
        return GetByFilter(bi => bi.BookId == bookId)
            ?? throw new ModelNotFoundException<BookImage>(bi => bi.BookId == bookId);
    }
}
