using Task4.Core.Interfaces.Repositories;
using Task4.Core.Models;

namespace Task4.DataAccess.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class, IBaseModel
{
    protected readonly List<T> Table;

    protected BaseRepository(List<T> table)
    {
        Table = table;
    }

    public Task<T?> GetAsync(int id)
    {
        return Task.FromResult(Table.FirstOrDefault(item => item.Id == id));
    }

    public Task<IReadOnlyList<T>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<T>>(Table);
    }

    public virtual Task<int> AddAsync(T newEntity)
    {
        var maxId = Table.Count == 0 ? 0 : Table.Max(item => item.Id);
        newEntity.Id = maxId + 1;
        Table.Add(newEntity);

        return Task.FromResult(newEntity.Id);
    }

    public Task UpdateAsync(T newEntity)
    {
        var entity = Table.First(item => item.Id == newEntity.Id);

        var index = Table.IndexOf(entity);
        Table[index] = newEntity;

        return Task.CompletedTask;
    }

    public Task RemoveAsync(int id)
    {
        var entity = Table.First(item => item.Id == id);
        Table.Remove(entity);

        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(int id)
    {
        return Task.FromResult(Table.Any(item => item.Id == id));
    }
}
