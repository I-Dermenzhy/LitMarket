namespace Abstractions.Repositories;
public interface IRepository<T>
{
    public IEnumerable<T> GetAll(bool isTracked = false);

    public void Add(T entity);

    public void Remove(T entity);
    public void RemoveRange(IEnumerable<T> entities);

    public void Update(T entity);
}
