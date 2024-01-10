using Domain.Models.Products;

namespace Abstractions.Repositories.Books;

public interface IBookImageRepository : IModelRepository<BookImage>
{
    public IEnumerable<BookImage> GetByBook(int bookId);
}
