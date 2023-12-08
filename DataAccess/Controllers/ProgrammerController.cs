using System.Runtime.CompilerServices;
using FakeDataSource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shared;
using Shared.Entities;

namespace DataAccess.Controllers;

public sealed class ProgrammerController : MyController
{
    private readonly IDataSource _dataSource;
    private readonly IMemoryCache _cache;

    private readonly TimeSpan _defaultCacheEntryValidity = TimeSpan.FromMinutes(10);
    
    public ProgrammerController(ILogger<ProgrammerController> logger, 
        IDataSource dataSource, IMemoryCache cache)
        : base(logger)
    {
        _dataSource = dataSource;
        _cache = cache;
    }

    [HttpGet]
    [Route(Routes.Programmer.GetAllRoute)]
    public IEnumerable<Programmer> GetAll()
    {
        LogCall(Routes.Programmer.GetAllRoute);

        return _dataSource.Programmers.Select(record => new Programmer()
        {
            Id = record.Id,
            Name = record.Name
        });
    }

    [HttpGet]
    [Route(Routes.Programmer.GetRoute)]
    public ActionResult<Programmer?> Get(int id)
    {
        LogCall(Routes.Programmer.GetRoute);

        var isCached = _cache.TryGetValue(id, out var cachedProgrammer);

        if (isCached)
        {
            _logger.LogInformation("Got a match from the cache for id: {Id}!", id);
            return Ok(cachedProgrammer as Programmer);
        }

        try
        {
            _logger.LogInformation("Retrieving from data source with id: {Id}", id);
            var programmer = _dataSource.Programmers
                .Where(record => record.Id == id)
                .Select(record => new Programmer()
                {
                    Id = record.Id,
                    Name = record.Name
                })
                //this is wanted to break if there is more than 1 result
                .Single();

            if (programmer.Id is null)
            {
                return BadRequest("Id missing - DB is corrupted - data integrity breach");
            }
            
            return Ok(_cache.Set(programmer.Id, programmer, _defaultCacheEntryValidity));
        }
        catch (Exception ex) when (ex is ArgumentException
                                       or InvalidOperationException)
        {
            return BadRequest(ex.Message);
        }
    }
}

public abstract class MyController : Controller
{
    protected readonly ILogger _logger;

    protected MyController(ILogger logger)
    {
        _logger = logger;
    }

    protected void LogCall(string endpoint,
        [CallerFilePath] string? callerFilePath = default)
    {
        var caller = callerFilePath?
            .Split("/")
            .LastOrDefault()?
            .Split(".")
            .FirstOrDefault();

        _logger.LogInformation("{Controller}/{Route} has been called",
            caller, endpoint);
    }
}