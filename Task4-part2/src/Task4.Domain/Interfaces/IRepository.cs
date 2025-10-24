using System.Linq.Expressions;
using Task4.Domain.Entities;

namespace Task4.Domain.Interfaces;

public interface IRepository<T> where T : class, IBaseEntity
{
    IQueryable<T> FindByAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);
    Task<T?> GetAsync(int id);
    IQueryable<T> GetAll();
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<bool> ExistsAsync(int id);
}
