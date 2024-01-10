using Domain.Models;

namespace Abstractions.Repositories;

public interface IModelRepository<T> : IRepository<T> where T : IModel
{
    public T GetById(int id, bool tracked = false);
    public void Remove(int id);
}

public interface IModelRepository<T, U> : IRepository<T> where T : IModel<U>
{
    public T GetById(U id, bool isTracked = false);
    public void Remove(U id);
}