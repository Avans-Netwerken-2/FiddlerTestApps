using Common;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        logger.LogInformation($"Received GET request at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Timestamp = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
    
    [HttpPost]
    public ActionResult<WeatherForecast> Post([FromBody] WeatherForecast newForecast)
    {
        logger.LogInformation(@$"Forecast received at {DateTime.Now:yyyy-MM-dd HH:mm:ss}. 
    TimeStamp: {newForecast.Timestamp}, 
    Temp: {newForecast.TemperatureC}, 
    Summary: {newForecast.Summary}");

        return CreatedAtAction(nameof(Post), newForecast);
    }
}