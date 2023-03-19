using Microsoft.AspNetCore.Mvc;
using Backend.Contracts;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<HealthDto> Get()
    {
        return Ok(new HealthDto("OK", DateTime.Now));
    }
}
