using System.ComponentModel.DataAnnotations;
using Task4.Core.DTOs;
using Task4.Core.Exceptions;
using Task4.Core.Interfaces.Repositories;
using Task4.Core.Interfaces.Services;
using Task4.Core.Models;

namespace Task4.Core.Services;

public class BookService(IRepository<Book> bookRepository) : IBookService
{
    private readonly IRepository<Book> _bookRepository = bookRepository;

    public async Task<BookDto> GetBookByIdAsync(int id)
    {
        var book = await _bookRepository.GetAsync(id) ??
            throw new NotFoundException($"Book with id {id} doesn't exist.");

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            PublishedYear = book.PublishedYear,
            AuthorId = book.AuthorId
        };
    }

    public async Task<IReadOnlyList<BookDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();

        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            PublishedYear = b.PublishedYear,
            AuthorId = b.AuthorId
        }
        ).ToList();
    }

    public async Task<int> AddAsync(BookForCreateDto book)
    {
        if (book.PublishedYear > DateTime.UtcNow.Year)
            throw new ValidationException("Book couldn't be published in the future.");

        var newBook = new Book
        {
            Title = book.Title,
            PublishedYear = book.PublishedYear,
            AuthorId = book.AuthorId
        };

        return await _bookRepository.AddAsync(newBook);
    }

    public async Task UpdateAsync(int id, BookForUpdateDto book)
    {
        if (book.PublishedYear > DateTime.UtcNow.Year)
            throw new ValidationException("Book couldn't be published in the future.");

        var isExist = await _bookRepository.ExistsAsync(id);

        if (!isExist)
            throw new NotFoundException($"Book with id {id} doesn't exist.");

        var updatedBook = new Book
        {
            Id = id,
            Title = book.Title,
            PublishedYear = book.PublishedYear,
            AuthorId = book.AuthorId
        };

        await _bookRepository.UpdateAsync(updatedBook);
    }

    public async Task DeleteAsync(int id)
    {
        var isExist = await _bookRepository.ExistsAsync(id);

        if (!isExist)
            throw new NotFoundException($"Book with id {id} doesn't exist.");

        await _bookRepository.RemoveAsync(id);
    }
}
