using AutoMapper;
using Restaurants.Application.Features.Restaurants.Commands;

namespace Restaurants.Application.Features.Restaurants.Dtos;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {

        CreateMap<CreateRestaurantCommand, Domain.Entities.Restaurant>()
            .ForMember(dest => dest.Address,
                       opt => opt.MapFrom(src => new Domain.Entities.Address
                       {
                           City = src.City,
                           Street = src.Street,
                           PostalCode = src.PostalCode
                       }));


        // Left will be source and right will be destination
        CreateMap<Domain.Entities.Restaurant, RestaurantDto>()
            .ForMember(dest => dest.Dishes,
                       opt => opt.MapFrom(src => src.Dishes))
            .ForMember(dest => dest.City,
                       opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(dest => dest.Street,
                        opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(dest => dest.PostalCode,
                        opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode));  
    }
}
