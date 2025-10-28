using Microsoft.Extensions.Logging;
using Restaurants.Application.Features.Dishes.Dtos;
using Restaurants.Application.Features.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Features.Restaurants;

public class RestaurantService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantService> logger) : IRestaurantService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
    {
        logger.LogInformation("Getting all restaurants");

        var restaurants = await restaurantsRepository.GetAllAsync();
        return restaurants.Select(x => x.ToRestaurantDto());
    }

    public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
    {
        logger.LogInformation("Getting restaurant by id");
        var restaurant = await restaurantsRepository.GetByIdAsync(id);
        return restaurant?.ToRestaurantDto();
    }
}
