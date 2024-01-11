using Domain.Models.Books;

namespace Abstractions.Repositories.Books;

public interface IBookImageRepository : IModelRepository<BookImage>
{
    public IEnumerable<BookImage> GetByBook(int bookId);
}
