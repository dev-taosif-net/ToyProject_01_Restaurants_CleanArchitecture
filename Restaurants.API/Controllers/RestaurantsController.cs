using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById([FromRoute]int id)
        {
            var restaurant = await restaurantService.GetRestaurantByIdAsync(id);

            return restaurant == null ? BadRequest() : Ok(restaurant);
        }
    }
}
