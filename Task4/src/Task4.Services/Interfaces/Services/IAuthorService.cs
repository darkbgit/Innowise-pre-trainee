using Task4.Core.DTOs;

namespace Task4.Core.Interfaces.Services;

public interface IAuthorService
{
    Task<AuthorDto> GetAuthorByIdAsync(int id);
    Task<IReadOnlyList<AuthorDto>> GetAllAuthorsAsync();
    Task<int> AddAsync(AuthorForCreateDto author);
    Task UpdateAsync(int id, AuthorForUpdateDto author);
    Task DeleteAsync(int id);
}
