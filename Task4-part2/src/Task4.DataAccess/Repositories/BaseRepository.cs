using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Task4.DataAccess.Data;
using Task4.Domain.Entities;
using Task4.Domain.Interfaces;

namespace Task4.DataAccess.Repositories;

internal abstract class BaseRepository<T> : IRepository<T> where T : class, IBaseEntity
{
    protected readonly Task4Context Db;
    protected readonly DbSet<T> Table;

    protected BaseRepository(Task4Context db)
    {
        Db = db;
        Table = Db.Set<T>();
    }

    public IQueryable<T> FindByAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
    {
        var result = Table.Where(predicate);
        if (includes.Length != 0)
        {
            result = includes
                .Aggregate(result,
                    (current, include)
                        => current.Include(include));
        }

        return result;
    }

    public async Task<T?> GetAsync(int id)
    {
        return await Table.SingleOrDefaultAsync(item => item.Id == id);
    }

    public IQueryable<T> GetAll()
    {
        return Table;
    }

    public async Task AddAsync(T newEntity)
    {
        await Table.AddAsync(newEntity);
    }

    public void Update(T entity)
    {
        Table.Update(entity);
    }

    public void Remove(T entity)
    {
        Table.Remove(entity);
    }

    public Task<bool> ExistsAsync(int id)
    {
        return Table.AnyAsync(item => item.Id == id);
    }

    public void Dispose()
    {
        Db?.Dispose();
        GC.SuppressFinalize(this);
    }
}
