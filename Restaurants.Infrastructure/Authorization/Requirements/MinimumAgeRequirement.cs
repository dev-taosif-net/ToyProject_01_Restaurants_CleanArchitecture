using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Features.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; } = minimumAge;
}


internal class MinimumAgeRequirementHandler ( IUserContext userContext) :  AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.CurrentUser();
        
        if(currentUser?.DateOfBirth == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }
        
        if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            context.Succeed(requirement);
        }
        
        else
        {
            context.Fail();
        }
        
        return Task.CompletedTask;
        
    }
}