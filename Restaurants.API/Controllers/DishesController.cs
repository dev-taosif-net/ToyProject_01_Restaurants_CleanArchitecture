using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Features.Dishes.Commands;
using Restaurants.Application.Features.Dishes.Dtos;

namespace Restaurants.API.Controllers;

[Route("api/restaurants/{restaurantId:int}/[controller]")]
[ApiController]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        var dishId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        return Ok();
    }
}
