using System.ComponentModel.DataAnnotations;
using Task4.Core.DTOs;
using Task4.Core.Exceptions;
using Task4.Core.Interfaces.Repositories;
using Task4.Core.Interfaces.Services;
using Task4.Core.Models;

namespace Task4.Core.Services;

public class AuthorService(IRepository<Author> authorRepository) : IAuthorService
{
    private readonly IRepository<Author> _authorRepository = authorRepository;

    public async Task<AuthorDto> GetAuthorByIdAsync(int id)
    {
        var author = await _authorRepository.GetAsync(id) ??
            throw new NotFoundException($"Author with id {id} doesn't exist.");

        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };
    }

    public async Task<IReadOnlyList<AuthorDto>> GetAllAuthorsAsync()
    {
        var authors = await _authorRepository.GetAllAsync();

        return authors.Select(a => new AuthorDto
        {
            Id = a.Id,
            Name = a.Name,
            DateOfBirth = a.DateOfBirth
        }
        ).ToList();
    }

    public async Task<int> AddAsync(AuthorForCreateDto author)
    {
        if (author.DateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ValidationException("Date of birth couldn't be in the future.");

        var newAuthor = new Author
        {
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };

        return await _authorRepository.AddAsync(newAuthor);
    }

    public async Task UpdateAsync(int id, AuthorForUpdateDto author)
    {
        if (author.DateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ValidationException("Date of birth couldn't be in the future.");

        var isExist = await _authorRepository.ExistsAsync(id);

        if (!isExist)
            throw new NotFoundException($"Author with id {id} doesn't exist.");

        var updatedAuthor = new Author
        {
            Id = id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };

        await _authorRepository.UpdateAsync(updatedAuthor);
    }

    public async Task DeleteAsync(int id)
    {
        var isExist = await _authorRepository.ExistsAsync(id);

        if (!isExist)
            throw new NotFoundException($"Author with id {id} doesn't exist.");

        await _authorRepository.RemoveAsync(id);
    }
}
