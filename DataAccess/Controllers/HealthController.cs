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
    [Route(Routes.Health.Ping)]
    public IActionResult Ping()
    {
        _logger.LogInformation("Health check called");
        return Ok();
    }
}