using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Features.Restaurants;
using Restaurants.Application.Features.Restaurants.Commands;
using Restaurants.Application.Features.Restaurants.Queries;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IRestaurantService restaurantService, IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantById() { Id = id});

            return restaurant == null ? BadRequest() : Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
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

            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);
        }

    }
}
