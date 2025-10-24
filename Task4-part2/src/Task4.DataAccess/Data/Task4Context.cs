using Microsoft.EntityFrameworkCore;
using Task4.Domain.Entities;

namespace Task4.DataAccess.Data;

public class Task4Context(DbContextOptions<Task4Context> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
}
