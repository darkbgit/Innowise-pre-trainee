using Task4.Core.Models;
using Task4.DataAccess.Data;

namespace Task4.DataAccess.Repositories;

public class BookRepository(InMemoryTask4Context context) : BaseRepository<Book>(context.Books)
{
    public override async Task<int> Add(Book newEntity)
    {
        if (!context.Authors.Any(a => a.Id == newEntity.AuthorId))
        {
            throw new Exception($"Author with id {newEntity.AuthorId} doesn't exist.");
        }

        return await base.Add(newEntity);
    }
}