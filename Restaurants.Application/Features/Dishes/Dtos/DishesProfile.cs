using AutoMapper;
using Restaurants.Application.Features.Dishes.Commands;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Features.Dishes.Dtos;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Domain.Entities.Dish, DishDto>();  // Left will be source and right will be destination
        CreateMap<CreateDishCommand , Dish>();

    }
}
