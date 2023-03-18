using Microsoft.AspNetCore.Mvc;

namespace biletmajster_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public string Get()
    {
        return $"{DateTime.Now} OK";
    }
}
