using AutoMapper;

using Domain.Dto.Books;
using Domain.Dto.Orders;
using Domain.Dto.Users;
using Domain.Models.Books;
using Domain.Models.Orders;
using Domain.Models.Products;
using Domain.Models.Users;

namespace Domain.Dto;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BookCategory, BookCategoryDto>().ReverseMap();
        CreateMap<BookImage, BookImageDto>().ReverseMap();
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<PriceList, PriceListDto>().ReverseMap();
        CreateMap<Shipping, ShippingDto>().ReverseMap();
    }
}
