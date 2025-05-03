using Microsoft.AspNetCore.Mvc;
using ResultPatternApi.Pattern;

namespace ResultPatternApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public EndpointResult<IEnumerable<WeatherForecast>, NoContentResult> Get()
        {
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            if ( !forecasts.Any() )
            {
                return Result<IEnumerable<WeatherForecast>, NoContentResult>.Success(forecasts);
            }
            else
            {
                return Result<IEnumerable<WeatherForecast>, NoContentResult>.Failure(NoContent());
            }
        }
    }
}
