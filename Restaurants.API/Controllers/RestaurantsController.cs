using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Features.Restaurants.Commands;
using Restaurants.Application.Features.Restaurants.Queries;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRestaurantById([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantById() { Id = id });

            return restaurant == null ? BadRequest() : Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
            ////Default model validation
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(createRestaurant);
            //}

            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new DeleteRestaurantCommand(id));

            return restaurant == false ? BadRequest() : NoContent();
        }

    }
}
