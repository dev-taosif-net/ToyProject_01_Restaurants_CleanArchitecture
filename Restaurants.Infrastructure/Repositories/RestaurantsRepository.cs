using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<int> CreateAsync(Restaurant restaurant)
    {
        dbContext.Restaurants.Add(restaurant);
        await dbContext.SaveChangesAsync();

        return restaurant.Id;
    }

    public async Task<bool> DeleteAsync(Restaurant restaurant)
    {
        dbContext.Restaurants.Remove(restaurant);
        await dbContext.SaveChangesAsync();
        return true ;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants
            .Include(x=>x.Dishes)
            .ToListAsync();
        return restaurants ?? [];
    }

    public async Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchText)
    {
        searchText = searchText?.ToLower();
        
        var restaurants = await dbContext.Restaurants.Where(x=> string.IsNullOrWhiteSpace(searchText) ||  x.Name.ToLower().Contains(searchText))
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
