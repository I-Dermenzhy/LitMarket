using Domain.Models.Books;

namespace Abstractions.Repositories.Books;
public interface IBookCategoryRepository : IModelRepository<BookCategory>
{
    public BookCategory GetByName(string name);
}

