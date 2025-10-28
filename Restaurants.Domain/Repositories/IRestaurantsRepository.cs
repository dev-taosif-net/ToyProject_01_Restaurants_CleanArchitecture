namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Entities.Restaurant>> GetAllAsync();
    Task<Entities.Restaurant?> GetByIdAsync(int id);
}
