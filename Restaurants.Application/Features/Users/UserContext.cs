using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.Features.Users;

public interface IUserContext
{
    CurrentUser? CurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? CurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user is null)
        {
            throw new InvalidOperationException("No HttpContext or User available");
        }

        if (user.Identity is not { IsAuthenticated: true })
        {
            return null;
        }
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = user.FindFirst(ClaimTypes.Email)?.Value;
        var roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value);
        var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
        var dateOfBirthString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;
        var dateOfBirth = dateOfBirthString == null 
            ? (DateOnly?)null 
            : DateOnly.ParseExact(dateOfBirthString, "yyyy-MM-dd");

        return new CurrentUser(id!, email!, roles , nationality, dateOfBirth);


    }
}
