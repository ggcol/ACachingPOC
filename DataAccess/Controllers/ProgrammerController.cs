using DataAccess.Utils;
using FakeDataSource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Shared;
using Shared.Entities;

namespace DataAccess.Controllers;

public sealed class ProgrammerController : Controller
{
    private readonly ILogger<ProgrammerController> _logger;
    private readonly IDataSource _dataSource;
    private readonly IMemoryCache _cache;
    private readonly IControllerMonitor _monitor;

    private readonly TimeSpan _defaultCacheEntryValidity =
        TimeSpan.FromSeconds(20);

    public ProgrammerController(ILogger<ProgrammerController> logger,
        IDataSource dataSource, IMemoryCache cache, IControllerMonitor monitor)
    {
        _logger = logger;
        _dataSource = dataSource;
        _cache = cache;
        _monitor = monitor;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var callId = context.HttpContext.TraceIdentifier;

        _logger.LogInformation(
            "{Controller}Controller/{Route} has been called, call id: {CallId}",
            context.ActionDescriptor.RouteValues["controller"],
            context.ActionDescriptor.RouteValues["action"],
            callId);

        _monitor.Start(callId);

        base.OnActionExecuting(context);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var callId = context.HttpContext.TraceIdentifier;

        _logger.LogInformation(
            "{Controller}Controller/{Route} end of call, call id: {CallId}",
            context.ActionDescriptor.RouteValues["controller"],
            context.ActionDescriptor.RouteValues["action"],
            callId);

        var elapsed = _monitor.Stop(callId);

        _logger.LogInformation(
            "{Controller}Controller/{Route} took {Elapsed} to execute for call {CallId}",
            context.ActionDescriptor.RouteValues["controller"],
            context.ActionDescriptor.RouteValues["action"], elapsed,
            callId);

        base.OnActionExecuted(context);
    }

    [HttpGet]
    [Route(Routes.Programmer.GetAll)]
    public ActionResult<IEnumerable<Programmer>> GetAll()
    {
        const string getAllCacheKey = "GetAll";

        var isCached =
            _cache.TryGetValue(getAllCacheKey, out var cachedProgrammers);
        
        if (isCached)
        {
            return Ok(cachedProgrammers);
        }

        var programmers = _dataSource.Programmers.Select(record =>
            new Programmer()
            {
                Id = record.Id,
                Name = record.Name,
                LinePerHour = record.LinePerHour,
                TimesInBurnout = record.TimesInBurnout
            });

        _cache.Set(getAllCacheKey, programmers, _defaultCacheEntryValidity);
        return Ok(programmers);
    }

    [HttpGet]
    [Route(Routes.Programmer.Get)]
    public ActionResult<Programmer?> Get(int id)
    {
        var isCached = _cache.TryGetValue(id, out var cachedProgrammer);

        if (isCached)
        {
            _logger.LogInformation("Got a match from the cache for id: {Id}!",
                id);
            return Ok(cachedProgrammer as Programmer);
        }

        try
        {
            _logger.LogInformation("Retrieving from data source with id: {Id}",
                id);
            var programmer = _dataSource.Programmers
                .Where(record => record.Id == id)
                .Select(record => new Programmer()
                {
                    Id = record.Id,
                    Name = record.Name,
                    LinePerHour = record.LinePerHour,
                    TimesInBurnout = record.TimesInBurnout
                })
                //this is wanted to break if there is more than 1 result
                .Single();

            if (programmer.Id is null)
            {
                return BadRequest(
                    "Id missing - DB is corrupted - data integrity breach");
            }

            _cache.Set(programmer.Id, programmer, _defaultCacheEntryValidity);

            return Ok(programmer);
        }
        catch (Exception ex) when (ex is ArgumentException
                                       or InvalidOperationException)
        {
            return BadRequest(ex.Message);
        }
    }
}