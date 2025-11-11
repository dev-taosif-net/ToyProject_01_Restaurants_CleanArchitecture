using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    //configuration
    //    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    //    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Information)
    //    .WriteTo.Console( outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {SourceContext}{NewLine}?? {Message:lj}{NewLine}{Exception}")
    //     .WriteTo.File(
    //        path: "Logs/log.Restaurant-.log",                    
    //        rollingInterval: RollingInterval.Day,      
    //        retainedFileCountLimit: 7,
    //        rollOnFileSizeLimit: true
    //    );
});


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
        options.DocumentPath = "/openapi/v1.json";
        options.Path = "/swagger";
        options.DocExpansion = "list";
    });
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapIdentityApi<User>();
app.UseAuthorization();

app.MapControllers();

app.Run();
