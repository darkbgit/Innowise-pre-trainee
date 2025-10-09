using Task4.Core.Models;

namespace Task4.DataAccess.Data;

public class InMemoryTask4Context
{
    public List<Author> Authors { get; set; } = [];
    public List<Book> Books { get; set; } = [];
}
