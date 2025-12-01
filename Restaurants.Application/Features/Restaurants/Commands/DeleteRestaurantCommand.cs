using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Features.Restaurants.Commands;

public record DeleteRestaurantCommand(int Id) : IRequest;

public class DeleteRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, IRestaurantAuthorizationService restaurantAuthorizationService ,
    ILogger<CreateRestaurantCommandHandler> logger) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Restaurant", request.Id.ToString());
        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            throw new ForbidException();


        await restaurantsRepository.DeleteAsync(restaurant);
        logger.LogInformation("Restaurant with Id {Id} deleted successfully.", request.Id);

    }
}

