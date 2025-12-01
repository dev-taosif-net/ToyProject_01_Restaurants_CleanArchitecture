using Restaurants.Application.Features.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
    {
        var user = userContext.CurrentUser();

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete || user!.IsInRole(UserRoles.Admin))
        {
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
          && user.Id == restaurant.OwnerId)
        {
            return true;
        }

        return false;

    }
}
