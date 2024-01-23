using AutoMapper;

using Domain.Dto.Books;
using Domain.Dto.Carts;
using Domain.Dto.Orders;
using Domain.Dto.Users;
using Domain.Models.Books;
using Domain.Models.Carts;
using Domain.Models.Orders;
using Domain.Models.Products;
using Domain.Models.Users;

namespace Domain.Dto;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BookCart, BookCartDto>()
            .ForMember(bcd => bcd.PricePerBook, opt => opt.MapFrom(src => src.GetPricePerBook()))
            .ForMember(bcd => bcd.TotalPrice, opt => opt.MapFrom(src => src.GetTotalPrice()))
            .ReverseMap();
        CreateMap<BookImage, BookImageDto>().ReverseMap();
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Genre, GenreDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderUpdateRequest, OrderUpdateRequestDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(oib => oib.TotalPrice, opt => opt.MapFrom(src => src.GetTotalPrice()))
            .ReverseMap();
        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<PriceList, PriceListDto>().ReverseMap();
        CreateMap<Shipping, ShippingDto>().ReverseMap();
    }
}
