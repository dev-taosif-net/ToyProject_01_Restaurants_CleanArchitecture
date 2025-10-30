using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Features.Restaurants;
using Restaurants.Application.Features.Restaurants.Dtos;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IRestaurantService restaurantService , IValidator<CreateRestaurantDto> _validator) : ControllerBase
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
            //var validationResult = await _validator.ValidateAsync(createRestaurant);

            //var validator = new CreateRestaurantDtoValidator();
            //var validationResult = await validator.ValidateAsync(createRestaurant);

            //if (!validationResult.IsValid)
            //{
            //    foreach (var error in validationResult.Errors)
            //    {
            //        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            //    }
            //    //return BadRequest(ModelState);
            //}
            ////Default model validation
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(createRestaurant);
            //}

            var id = await restaurantService.CreateRestaurantAsync(createRestaurant);
            return CreatedAtAction(nameof(GetRestaurantById), new { id = id }, null);
        }

    }
}
