using Task4.Domain.Entities;
using Task4.Domain.Interfaces;

namespace Task4.DataAccess.Data;

public class UnitOfWork(Task4Context context,
    IRepository<Author> authorRepository,
    IRepository<Book> bookRepository) : IUnitOfWork
{
    private readonly Task4Context _context = context;

    public IRepository<Author> Authors { get; } = authorRepository;
    public IRepository<Book> Books { get; } = bookRepository;

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
