namespace Restaurants.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string MinimumAge = "HasNationality";
    public const string CreatedAtleast2Restaurants = "CreatedAtleast2Restaurants";
}

public static class AppClaimTypes
{
    public const string Nationality = "Nationality";
    public const string DateOfBirth = "DateOfBirth";
}