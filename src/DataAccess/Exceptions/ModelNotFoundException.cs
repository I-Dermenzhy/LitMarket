using Domain.Models;

using System.Linq.Expressions;

namespace DataAccess.Exceptions;

public class ModelNotFoundException<T, U>
    : NotFoundException<T> where T : IModel<U>
{
    public ModelNotFoundException(string message)
        : base(message)
    {
    }

    public ModelNotFoundException(U id)
        : this($"The {typeof(T)} with the id '{id}' was not found.")
    {
    }

    public ModelNotFoundException(Expression<Func<T, bool>> searchFilters)
        : base(searchFilters)
    {
    }

    public ModelNotFoundException(T model)
        : base(model)
    {
        Model = model ?? throw new ArgumentNullException(nameof(model));
    }

    public T? Model { get; init; }
}

public class ModelNotFoundException<T>
    : ModelNotFoundException<T, int> where T : IModel<int>
{
    public ModelNotFoundException(string message)
        : base(message)
    {
    }

    public ModelNotFoundException(int id)
        : base($"The {typeof(T)} with the id '{id}' was not found.")
    {
    }

    public ModelNotFoundException(Expression<Func<T, bool>> searchFilters)
        : base(searchFilters)
    {
    }

    public ModelNotFoundException(T model)
        : base(model)
    {
        Model = model ?? throw new ArgumentNullException(nameof(model));
    }
}