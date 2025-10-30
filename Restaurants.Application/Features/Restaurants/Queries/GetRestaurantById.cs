using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Features.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Features.Restaurants.Queries;

public class GetRestaurantById : IRequest<RestaurantDto?>
{
    public int Id { get; set; }
}


public class GetRestaurantByIdHandler(ILogger<GetRestaurantByIdHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetRestaurantById, RestaurantDto?>
{
    public async Task<RestaurantDto?> Handle(GetRestaurantById request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting restaurant by id");
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        ////var restaurantDto = restaurant?.ToRestaurantDto(); // Manual mapping
        var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);
        return restaurantDto;
    }
}
