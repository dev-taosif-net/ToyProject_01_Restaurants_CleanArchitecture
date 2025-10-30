using Microsoft.AspNetCore.Builder;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApi(); // Optional, for NSwag middleware to serve the document
    app.UseSwaggerUi(options =>
    {
        // Point NSwag UI to your OpenAPI endpoint
        options.DocumentPath = "/openapi/v1.json";
        options.Path = "/swagger"; // UI available at /swagger
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
