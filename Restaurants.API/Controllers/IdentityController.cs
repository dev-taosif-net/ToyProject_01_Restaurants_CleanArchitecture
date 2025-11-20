using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Features.Users.Commands.UpdateUserDetails;

namespace Restaurants.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}
