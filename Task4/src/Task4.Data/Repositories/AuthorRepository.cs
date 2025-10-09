using Task4.Core.Models;
using Task4.DataAccess.Data;

namespace Task4.DataAccess.Repositories;

public class AuthorRepository(InMemoryTask4Context context) : BaseRepository<Author>(context.Authors)
{
}
