using Task4.Core.Models;
using Task4.DataAccess.Data;

namespace Task4.DataAccess.Repositories;

public class BookRepository(InMemoryTask4Context context) : BaseRepository<Book>(context.Books)
{
}