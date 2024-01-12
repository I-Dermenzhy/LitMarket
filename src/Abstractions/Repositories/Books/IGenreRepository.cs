using Domain.Models.Books;

namespace Abstractions.Repositories.Books;
public interface IGenreRepository : IModelRepository<Genre>
{
    public Genre GetByName(string name);
}

