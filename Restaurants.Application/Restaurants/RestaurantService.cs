using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

public class RestaurantService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantService> logger) : IRestaurantService
{
    public Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
    {
        logger.LogInformation("Getting all restaurants");
        return restaurantsRepository.GetAllAsync();
    }

    public Task<Restaurant?> GetRestaurantByIdAsync(int id)
    {
        logger.LogInformation("Getting restaurant by id");
        return restaurantsRepository.GetByIdAsync(id);
    }
}
