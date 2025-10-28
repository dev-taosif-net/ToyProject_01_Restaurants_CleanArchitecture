using Restaurants.Application.Features.Restaurants.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Features.Restaurants
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
        Task<RestaurantDto?> GetRestaurantByIdAsync(int id);
    }
}