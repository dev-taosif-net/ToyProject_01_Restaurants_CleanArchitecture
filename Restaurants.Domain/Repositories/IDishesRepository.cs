namespace Restaurants.Domain.Repositories;

public interface IDishesRepository
{
    Task<IEnumerable<Entities.Dish>> GetAllByRestaurantIdAsync(int restaurantId);
    Task<Entities.Dish?> GetByIdAsync(int id);
    Task<int> CreateAsync(Entities.Dish dish);
    Task DeleteAsync(Entities.Dish dish);
}
