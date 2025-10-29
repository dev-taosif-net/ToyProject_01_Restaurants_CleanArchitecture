using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Features.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Features.Restaurants;

public class RestaurantService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantService> logger, IMapper mapper) : IRestaurantService
{

    public async Task<int> CreateRestaurantAsync(CreateRestaurantDto createRestaurant)
    {

        logger.LogInformation("Creating a new restaurant");
        var restaurantEntity = mapper.Map<Domain.Entities.Restaurant>(createRestaurant);

        var id = await restaurantsRepository.CreateAsync(restaurantEntity);

        return id ;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
    {
        logger.LogInformation("Getting all restaurants");

        var restaurants = await restaurantsRepository.GetAllAsync();

        //var restaurantDtos = restaurants.Select(x => x.ToRestaurantDto());  //Manual
        var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        return restaurantDtos;
    }

    public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
    {
        logger.LogInformation("Getting restaurant by id");
        var restaurant = await restaurantsRepository.GetByIdAsync(id);

        ////var restaurantDto = restaurant?.ToRestaurantDto(); // Manual mapping
        var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);
        return restaurantDto;
    }
}
