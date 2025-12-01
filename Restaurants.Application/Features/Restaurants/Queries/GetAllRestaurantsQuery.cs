using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Features.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Features.Restaurants.Queries;

public record GetAllRestaurantsQuery(string? searchPhrase) : IRequest<IEnumerable<RestaurantDto>>;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository)
    : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants");

        var restaurants = await restaurantsRepository.GetAllMatchingAsync(request.searchPhrase);

        //var restaurantDtos = restaurants.Select(x => x.ToRestaurantDto());  //Manual
        var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        return restaurantDtos;
    }

}

