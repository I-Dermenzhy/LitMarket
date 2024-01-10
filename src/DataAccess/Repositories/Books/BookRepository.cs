using Abstractions.Repositories.Books;

using DataAccess.Data;

using Domain.Models.Books;

namespace DataAccess.Repositories.Books;

public class BookRepository(LibMarketDbContext db)
    : ModelRepository<Book>(db), IBookRepository
{
}

