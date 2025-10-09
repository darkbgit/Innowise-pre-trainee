using Task4.Core.DTOs;
using Task4.Core.Interfaces.Repositories;
using Task4.Core.Interfaces.Services;
using Task4.Core.Models;

namespace Task4.Core.Services;

public class AuthorService : IAuthorService
{
    private readonly IRepository<Author> _authorRepository;

    public AuthorService(IRepository<Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<AuthorDto?> GetAuthorById(int id)
    {
        var author = await _authorRepository.Get(id);

        return author == null ? null : new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };
    }

    public async Task<IReadOnlyList<AuthorDto>> GetAllAuthors()
    {
        var authors = await _authorRepository.GetAll();

        return authors.Select(a => new AuthorDto
        {
            Id = a.Id,
            Name = a.Name,
            DateOfBirth = a.DateOfBirth
        }
        ).ToList();
    }

    public async Task<int> Add(AuthorForCreateDto author)
    {
        var newAuthor = new Author
        {
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };

        return await _authorRepository.Add(newAuthor);
    }

    public async Task<bool> Update(int id, AuthorForUpdateDto author)
    {
        var newAuthor = new Author
        {
            Id = id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };

        return await _authorRepository.Update(newAuthor);
    }

    public async Task<bool> Delete(int id)
    {
        return await _authorRepository.Remove(id);
    }
}
