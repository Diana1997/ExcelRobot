using Microsoft.AspNetCore.Mvc;

namespace ExcelRobot.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return null;
    }
}