
using Restaurants.Application.Features.Restaurants.Commands;

namespace Restaurants.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<CreateRestaurantCommandHandler> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "An unhandled exception has occurred while processing the request.");
            context.Response.StatusCode = 500;
            //await context.Response.WriteAsJsonAsync(new { Error = "An unexpected error occurred. Please try again later." });
            await context.Response.WriteAsJsonAsync( "An unexpected error occurred. Please try again later." );
        }


       
    }
}
