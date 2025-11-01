using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Features.Restaurants.Commands;

public record DeleteRestaurantCommand(int Id) : IRequest<bool>;

public class DeleteRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, ILogger<CreateRestaurantCommandHandler> logger) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
        if (restaurant is null)
        {
            logger.LogWarning("Restaurant with Id {Id} not found.", request.Id);
            return false;
        }
        await restaurantsRepository.DeleteAsync(restaurant);
        logger.LogInformation("Restaurant with Id {Id} deleted successfully.", request.Id);
        return true;
    }
}

