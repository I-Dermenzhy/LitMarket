using Abstractions.Repositories.Carts;

using DataAccess.Data;
using DataAccess.Exceptions;

using Domain.Models.Carts;

namespace DataAccess.Repositories.Carts;
public class BookCartRepository(LitMarketDbContext db)
    : ModelRepository<BookCart>(db), IBookCartRepository
{
    public BookCart GetByCustomer(string customerId)
    {
        return GetByFilter(bc => bc.CustomerId == customerId).FirstOrDefault()
            ?? throw new ModelNotFoundException<BookCart>(bc => bc.CustomerId == customerId);
    }

    public BookCart GetByCustomerAndBook(string customerId, int bookId)
    {
        return GetByFilter(bc => bc.CustomerId == customerId && bc.BookId == bookId).FirstOrDefault()
            ?? throw new ModelNotFoundException<BookCart>(bc => bc.CustomerId == customerId && bc.BookId == bookId);
    }
}
