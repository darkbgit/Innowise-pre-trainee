using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task4.Core.DTOs;
using Task4.Core.Exceptions;
using Task4.Core.Interfaces.Services;
using Task4.Domain.Entities;
using Task4.Domain.Interfaces;

namespace Task4.Core.Services;

internal class BookService(IUnitOfWork unitOfWork, IMapper mapper) : IBookService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<BookDto> GetBookByIdAsync(int id)
    {
        var book = await _unitOfWork.Books
            .FindByAsync(b => b.Id == id, b => b.Author)
            .SingleOrDefaultAsync()
            ?? throw new NotFoundException($"Book with id {id} doesn't exist.");

        return _mapper.Map<BookDto>(book);
    }

    public async Task<IReadOnlyList<BookDto>> GetAllBooksAsync()
    {
        var books = await _unitOfWork.Books.GetAll()
            .Include(b => b.Author)
            .Select(b => _mapper.Map<BookDto>(b))
            .ToListAsync();

        return books.AsReadOnly();
    }

    public async Task<IReadOnlyList<BookDto>> SearchBooksAsync(int? authorId, int? publishedAfter, int? publishedBefore)
    {
        Expression<Func<Book, bool>> filter = a => true;
        var parameter = Expression.Parameter(typeof(Book));

        if (authorId.HasValue)
        {
            Expression<Func<Book, bool>> authorFilter = a => a.AuthorId == authorId.Value;
            filter = Expression.Lambda<Func<Book, bool>>(Expression.AndAlso(Expression.Invoke(filter, parameter), Expression.Invoke(authorFilter, parameter)), parameter);
        }

        if (publishedAfter.HasValue || publishedBefore.HasValue)
        {
            Expression<Func<Book, bool>> publishedYearFilter = a => a.PublishedYear >= (publishedAfter ?? int.MinValue) && a.PublishedYear <= (publishedBefore ?? int.MaxValue);
            filter = Expression.Lambda<Func<Book, bool>>(Expression.AndAlso(Expression.Invoke(filter, parameter), Expression.Invoke(publishedYearFilter, parameter)), parameter);
        }

        var books = _unitOfWork.Books.FindByAsync(filter);

        var result = await books
            .Include(b => b.Author)
            .Select(b => _mapper.Map<BookDto>(b))
            .ToListAsync();

        return result.AsReadOnly();
    }

    public async Task<int> AddAsync(BookForCreateDto book)
    {
        if (book.PublishedYear > DateTime.UtcNow.Year)
            throw new ValidationException("Book couldn't be published in the future.");

        if (!await _unitOfWork.Authors.ExistsAsync(book.AuthorId))
            throw new NotFoundException($"Author with id {book.AuthorId} doesn't exist.");

        var newBook = _mapper.Map<Book>(book);

        await _unitOfWork.Books.AddAsync(newBook);
        await _unitOfWork.SaveChangesAsync();

        return newBook.Id;
    }

    public async Task UpdateAsync(int id, BookForUpdateDto book)
    {
        if (book.PublishedYear > DateTime.UtcNow.Year)
            throw new ValidationException("Book couldn't be published in the future.");

        var bookDb = await _unitOfWork.Books.GetAsync(id) ??
            throw new NotFoundException($"Book with id {id} doesn't exist.");

        if (book.AuthorId.HasValue && !await _unitOfWork.Authors.ExistsAsync(book.AuthorId.Value))
            throw new NotFoundException($"Author with id {book.AuthorId.Value} doesn't exist.");

        _mapper.Map(book, bookDb);

        _unitOfWork.Books.Update(bookDb);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Books.GetAsync(id) ??
            throw new NotFoundException($"Book with id {id} doesn't exist.");

        _unitOfWork.Books.Remove(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}