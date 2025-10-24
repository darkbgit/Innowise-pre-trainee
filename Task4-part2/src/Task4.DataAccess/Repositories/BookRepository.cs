using Task4.DataAccess.Data;
using Task4.Domain.Entities;

namespace Task4.DataAccess.Repositories;

internal class BookRepository(Task4Context context) : BaseRepository<Book>(context)
{
}