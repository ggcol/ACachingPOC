using Microsoft.AspNetCore.Mvc;
using Shared;

namespace DataAccess.Controllers;

public class HealthController : Controller
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    [Route(Routes.Health.Get)]
    public IActionResult Get()
    {
        _logger.LogInformation("Health check called");
        return Ok("Server is up and running");
    }
}