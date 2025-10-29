using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public Task<int> CreateAsync(Restaurant restaurant)
    {
        dbContext.Restaurants.Add(restaurant);
        return dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants
            .Include(x=>x.Dishes)
            .ToListAsync();
        return restaurants ?? [];
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants
            .Include(x => x.Dishes)
            .FirstOrDefaultAsync( x=>x.Id == id);
        return restaurant;
    }
}
