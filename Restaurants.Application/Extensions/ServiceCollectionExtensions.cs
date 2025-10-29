using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Features.Restaurants;
using AutoMapper;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddAutoMapper(cfg => { }, applicationAssembly);


        //Services
        services.AddScoped<IRestaurantService, RestaurantService>();
    }
}
