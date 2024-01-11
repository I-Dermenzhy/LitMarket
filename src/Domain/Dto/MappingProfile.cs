using AutoMapper;

using Domain.Dto.Books;
using Domain.Models.Books;

namespace Domain.Dto;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BookCategory, BookCategoryDto>().ReverseMap();
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<BookImage, BookImageDto>().ReverseMap();
    }
}
