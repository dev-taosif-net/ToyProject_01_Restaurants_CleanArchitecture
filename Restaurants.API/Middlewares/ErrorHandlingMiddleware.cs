
using Restaurants.Application.Features.Restaurants.Commands;
using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<CreateRestaurantCommandHandler> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException notFound)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(notFound.Message);

            logger.LogWarning(notFound.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception has occurred while processing the request.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //await context.Response.WriteAsJsonAsync(new { Error = "An unexpected error occurred. Please try again later." });
            await context.Response.WriteAsJsonAsync( "An unexpected error occurred. Please try again later." );
        }


       
    }
}
