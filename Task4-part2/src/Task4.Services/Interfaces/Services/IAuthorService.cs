using Task4.Core.DTOs;

namespace Task4.Core.Interfaces.Services;

public interface IAuthorService
{
    Task<AuthorDto> GetAuthorByIdAsync(int id);
    Task<IReadOnlyList<AuthorDto>> GetAllAuthorsAsync();
    Task<IReadOnlyList<AuthorDto>> SearchAuthorsAsync(string? name, DateOnly? bornAfter);
    Task<IReadOnlyList<AuthorWithBooksNumberDto>> GetAllAuthorsWithBooksNumberAsync();
    Task<int> AddAsync(AuthorForCreateDto author);
    Task UpdateAsync(int id, AuthorForUpdateDto author);
    Task DeleteAsync(int id);
}
