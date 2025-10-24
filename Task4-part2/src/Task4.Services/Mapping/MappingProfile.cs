using AutoMapper;
using Task4.Core.DTOs;
using Task4.Domain.Entities;

namespace Task4.Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth)));
        CreateMap<AuthorDto, Author>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToDateTime(TimeOnly.MinValue)));

        CreateMap<Author, AuthorWithBooksNumberDto>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth)))
            .ForMember(dest => dest.BooksNumber, opt => opt.MapFrom(src => src.Books.Count));

        CreateMap<AuthorForCreateDto, Author>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToDateTime(TimeOnly.MinValue)));

        CreateMap<AuthorForUpdateDto, Author>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth!.Value.ToDateTime(TimeOnly.MinValue)))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));

        CreateMap<BookForCreateDto, Book>();
        CreateMap<BookForUpdateDto, Book>()
            .ForMember(dest => dest.AuthorId, opt => opt.PreCondition(src => src.AuthorId.HasValue))
            .ForMember(dest => dest.PublishedYear, opt => opt.PreCondition(src => src.PublishedYear.HasValue))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
