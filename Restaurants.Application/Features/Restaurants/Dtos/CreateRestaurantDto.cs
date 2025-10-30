using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Features.Restaurants.Dtos;

public class CreateRestaurantDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }

    [EmailAddress]
    public string? ContactEmail { get; set; }
    [Phone]
    public string? ContactNumber { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
}
