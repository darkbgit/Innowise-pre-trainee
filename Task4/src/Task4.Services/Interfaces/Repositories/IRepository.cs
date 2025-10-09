using Task4.Core.Models;

namespace Task4.Core.Interfaces.Repositories;

public interface IRepository<T> where T : class, IBaseModel
{
    Task<T?> Get(int id);
    Task<IReadOnlyList<T>> GetAll();
    Task<int> Add(T entity);
    Task<bool> Update(T entity);
    Task<bool> Remove(int id);
}
