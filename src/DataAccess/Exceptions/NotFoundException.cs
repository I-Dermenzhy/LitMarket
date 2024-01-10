using System.Linq.Expressions;

namespace DataAccess.Exceptions;

public class NotFoundException<T>(string message) : Exception(message)
{
    public NotFoundException(Expression<Func<T, bool>> searchFilters)
        : this($"An entity with the following search filters: {searchFilters} was not found.")
    {
        SearchFilters = searchFilters ?? throw new ArgumentNullException(nameof(searchFilters));
    }

    public NotFoundException(T entity)
        : this($"The entity {entity} was not found.")
    {
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public T? Entity { get; init; }

    public Expression<Func<T, bool>>? SearchFilters { get; init; }
}
