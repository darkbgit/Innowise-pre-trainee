using Task4.Domain.Entities;

namespace Task4.Domain.Interfaces;

public interface IUnitOfWork
{
    IRepository<Author> Authors { get; }
    IRepository<Book> Books { get; }
    Task<int> SaveChangesAsync();
}
