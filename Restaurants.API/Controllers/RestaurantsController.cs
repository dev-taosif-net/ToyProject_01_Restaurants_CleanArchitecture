using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Features.Restaurants;
using Restaurants.Application.Features.Restaurants.Dtos;

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

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurant)
        {
            //Default model validation
            if(!ModelState.IsValid)
            {
                return BadRequest(createRestaurant);
            }

            var id = await restaurantService.CreateRestaurantAsync(createRestaurant);
            return CreatedAtAction(nameof(GetRestaurantById), new { id = id }, null);
        }

    }
}
