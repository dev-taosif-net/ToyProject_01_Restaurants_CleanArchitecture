using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DefaultModelsExpandDepth = -1;  //No schema will show by default
        options.DocExpansion = "list";
    });
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapGroup("api/Identity/").WithTags("Identity").MapIdentityApi<User>();
app.UseAuthorization();

app.MapControllers();

app.Run();
