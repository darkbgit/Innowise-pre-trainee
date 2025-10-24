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

internal class AuthorService(IUnitOfWork unitOfWork, IMapper mapper) : IAuthorService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<AuthorDto> GetAuthorByIdAsync(int id)
    {
        var author = await _unitOfWork.Authors.GetAsync(id) ??
                        throw new NotFoundException($"Author with id {id} doesn't exist.");

        return _mapper.Map<AuthorDto>(author);
    }

    public async Task<IReadOnlyList<AuthorDto>> GetAllAuthorsAsync()
    {
        var authors = await _unitOfWork.Authors.GetAll()
            .Select(a => _mapper.Map<AuthorDto>(a))
            .ToListAsync();

        return authors.AsReadOnly();
    }

    public async Task<IReadOnlyList<AuthorDto>> SearchAuthorsAsync(string? name, DateOnly? bornAfter)
    {
        Expression<Func<Author, bool>> filter = a => true;
        var parameter = Expression.Parameter(typeof(Author));

        if (!string.IsNullOrWhiteSpace(name))
        {
            Expression<Func<Author, bool>> nameFilter = a => a.Name.StartsWith(name);
            filter = Expression.Lambda<Func<Author, bool>>(Expression.AndAlso(Expression.Invoke(filter, parameter), Expression.Invoke(nameFilter, parameter)), parameter);
        }

        if (bornAfter.HasValue)
        {
            Expression<Func<Author, bool>> dateFilter = a => a.DateOfBirth > bornAfter.Value.ToDateTime(TimeOnly.MinValue);
            filter = Expression.Lambda<Func<Author, bool>>(Expression.AndAlso(Expression.Invoke(filter, parameter), Expression.Invoke(dateFilter, parameter)), parameter);
        }

        var authors = _unitOfWork.Authors.FindByAsync(filter);

        var result = await authors
            .Select(a => _mapper.Map<AuthorDto>(a))
            .ToListAsync();

        return result.AsReadOnly();
    }

    public async Task<IReadOnlyList<AuthorWithBooksNumberDto>> GetAllAuthorsWithBooksNumberAsync()
    {
        var authors = await _unitOfWork.Authors.GetAll()
            .Include(a => a.Books)
            .Select(a => _mapper.Map<AuthorWithBooksNumberDto>(a))
            .ToListAsync();

        return authors.AsReadOnly();
    }

    public async Task<int> AddAsync(AuthorForCreateDto author)
    {
        if (author.DateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ValidationException("Date of birth couldn't be in the future.");

        var newAuthor = _mapper.Map<Author>(author);

        await _unitOfWork.Authors.AddAsync(newAuthor);
        await _unitOfWork.SaveChangesAsync();

        return newAuthor.Id;
    }

    public async Task UpdateAsync(int id, AuthorForUpdateDto author)
    {
        if (author.DateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ValidationException("Date of birth couldn't be in the future.");

        var authorDb = await _unitOfWork.Authors.GetAsync(id) ??
            throw new NotFoundException($"Author with id {id} doesn't exist.");

        authorDb = _mapper.Map(author, authorDb);

        _unitOfWork.Authors.Update(authorDb);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Authors.GetAsync(id) ??
            throw new NotFoundException($"Author with id {id} doesn't exist.");

        _unitOfWork.Authors.Remove(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}
