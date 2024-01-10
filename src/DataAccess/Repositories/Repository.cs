using Abstractions.Repositories;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace DataAccess.Repositories;

public abstract class Repository<T>(DbContext db) : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = db.Set<T>();

    public IEnumerable<T> GetAll(bool isTracked = false)
    {
        return isTracked
            ? _dbSet.ToList()
            : _dbSet.AsNoTracking().ToList();
    }

    public void Add(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbSet.Add(entity);
    }

    public void Remove(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        _dbSet.RemoveRange(entities);
    }

    public void Update(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbSet.Update(entity);
    }

    protected IEnumerable<T> GetByFilter(Expression<Func<T, bool>> filter,
        bool isTracked = false)
    {
        IQueryable<T> query = _dbSet.Where(filter);

        return isTracked
            ? query.ToList()
            : query.AsNoTracking().ToList();
    }
}