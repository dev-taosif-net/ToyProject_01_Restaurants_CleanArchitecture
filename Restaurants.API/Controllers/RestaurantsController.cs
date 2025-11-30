using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Features.Restaurants.Commands;
using Restaurants.Application.Features.Restaurants.Dtos;
using Restaurants.Application.Features.Restaurants.Queries;
using Restaurants.Application.Features.Users;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantsController(IMediator mediator, IUserContext userContext ) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Owner},{UserRoles.Admin}")]

        public async Task<ActionResult<IEnumerator<RestaurantDto>>> GetAllRestaurants()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PolicyNames.HasNationality)]
        public async Task<ActionResult<RestaurantDto?>> GetRestaurantById([FromRoute] int id)
        {
            var user = userContext.CurrentUser();
            
            var restaurant = await mediator.Send(new GetRestaurantById() { Id = id });
            return Ok(restaurant);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = UserRoles.Owner)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));

            return NoContent();
        }

    }
}
