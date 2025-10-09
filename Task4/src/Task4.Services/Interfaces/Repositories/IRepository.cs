using Task4.Core.Models;

namespace Task4.Core.Interfaces.Repositories;

public interface IRepository<T> where T : class, IBaseModel
{
    Task<T?> GetAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<int> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(int id);
    Task<bool> ExistsAsync(int id);
}
