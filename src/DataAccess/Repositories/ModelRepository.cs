using Abstractions.Repositories;

using DataAccess.Exceptions;

using Domain.Models;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace DataAccess.Repositories;

public abstract class ModelRepository<T, U>(DbContext db)
    : Repository<T>(db), IModelRepository<T, U> where T : class, IModel<U>
{
    public T GetById(U id, bool isTracked = false)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));

        var expression = (Expression<Func<T, bool>>)
            (m => id.Equals(m.Id));

        return GetByFilter(expression, isTracked: isTracked).FirstOrDefault()
                ?? throw new ModelNotFoundException<T, U>(id.ToString()!);
    }

    public void Remove(U id)
    {
        var model = GetById(id);

        Remove(model);
    }
}

public abstract class ModelRepository<T>(DbContext db)
    : ModelRepository<T, int>(db) where T : class, IModel<int>
{
}