using Task4.Core.DTOs;

namespace Task4.Core.Interfaces.Services;

public interface IAuthorService
{
    Task<AuthorDto?> GetAuthorById(int id);
    Task<IReadOnlyList<AuthorDto>> GetAllAuthors();
    Task<int> Add(AuthorForCreateDto author);
    Task<bool> Update(int id, AuthorForUpdateDto author);
    Task<bool> Delete(int id);
}
