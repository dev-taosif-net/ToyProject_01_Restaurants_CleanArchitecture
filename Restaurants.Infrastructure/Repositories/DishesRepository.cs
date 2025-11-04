using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

public class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
{
    public async Task<int> CreateAsync(Dish dish)
    {
        dbContext.Dishes.Add(dish);
        await dbContext.SaveChangesAsync();

        return dish.Id;
    }

    public async Task DeleteAsync(Dish dish)
    {
        dbContext.Dishes.Remove(dish);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Dish>> GetAllByRestaurantIdAsync(int restaurantId)
    {
       return await dbContext.Dishes
            .Where(d => d.RestaurantId == restaurantId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Dish?> GetByIdAsync(int id)
    {
        return await dbContext.Dishes.FindAsync(id);
    }
}
