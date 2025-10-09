using Task4.Core.DTOs;

namespace Task4.Core.Interfaces.Services;

public interface IBookService
{
    Task<BookDto> GetBookById(int id);
    Task<IReadOnlyList<BookDto>> GetAllBooks();
    Task Add(BookDto book);
    Task Update(BookDto book);
    Task Delete(int id);
}
