using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Services;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IGuidService _guidService;

        public WeatherForecastController(IGuidService guidService)
        {
            _guidService = guidService;
        }

        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // GET: api/WeatherForecast/guid
        [HttpGet("guid1", Name = "GetGuid1")]
        public ActionResult<string> GetGuid()
        {
            var guid1 = _guidService.GetGuid1();
            var guid2 = _guidService.GetGuid2();
            return Ok( $"{guid1.ToString()}          {guid2.ToString()}" );
        }

        // GET: api/WeatherForecast/guid
        [HttpGet("guid2", Name = "GetGuid2")]
        public ActionResult<string> GetGuid2()
        {
            var guid1 = _guidService.GetGuid1();
            var guid2 = _guidService.GetGuid2();
            return Ok($"{guid1.ToString()}          {guid2.ToString()}");
        }
    }
}
