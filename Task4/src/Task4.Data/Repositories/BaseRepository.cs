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

    public Task<T?> Get(int id)
    {
        return Task.FromResult(Table.FirstOrDefault(item => item.Id == id));
    }

    public Task<IReadOnlyList<T>> GetAll()
    {
        return Task.FromResult<IReadOnlyList<T>>(Table);
    }

    public virtual Task<int> Add(T newEntity)
    {
        var maxId = Table.Count == 0 ? 0 : Table.Max(item => item.Id);
        newEntity.Id = maxId + 1;
        Table.Add(newEntity);

        return Task.FromResult(newEntity.Id);
    }

    public Task<bool> Update(T newEntity)
    {
        var entity = Table.FirstOrDefault(item => item.Id == newEntity.Id);
        if (entity == null)
        {
            return Task.FromResult(false);
        }
        var index = Table.IndexOf(entity);
        Table[index] = newEntity;

        return Task.FromResult(true);
    }

    public Task<bool> Remove(int id)
    {
        var entity = Table.FirstOrDefault(item => item.Id == id);
        if (entity == null)
        {
            return Task.FromResult(false);
        }
        Table.Remove(entity);

        return Task.FromResult(true);
    }
}
