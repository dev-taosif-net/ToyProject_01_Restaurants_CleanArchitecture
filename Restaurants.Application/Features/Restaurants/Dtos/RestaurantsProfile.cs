using AutoMapper;

namespace Restaurants.Application.Features.Restaurants.Dtos;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {

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
