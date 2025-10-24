using Task4.Core.DTOs;

namespace Task4.Core.Interfaces.Services;

public interface IBookService
{
    Task<BookDto> GetBookByIdAsync(int id);
    Task<IReadOnlyList<BookDto>> GetAllBooksAsync();
    Task<IReadOnlyList<BookDto>> SearchBooksAsync(int? authorId, int? publishedAfter, int? publishedBefore);
    Task<int> AddAsync(BookForCreateDto book);
    Task UpdateAsync(int id, BookForUpdateDto book);
    Task DeleteAsync(int id);
}
