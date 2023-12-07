using FakeDataSource;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;

namespace DataAccess.Controllers;

public sealed class ProgrammerController : Controller
{
    private const string GetAllRoute = "GetAll";

    private readonly ILogger<ProgrammerController> _logger;
    private readonly IDataSource _dataSource;

    public ProgrammerController(ILogger<ProgrammerController> logger,
        IDataSource dataSource)
    {
        _logger = logger;
        _dataSource = dataSource;
    }

    [HttpGet]
    [Route(GetAllRoute)]
    public IEnumerable<Programmer> GetAll()
    {
        _logger.LogInformation("{Controller}/{Route} has been called",
            nameof(ProgrammerController), GetAllRoute);

        return _dataSource.Programmers.Select(record => new Programmer()
        {
            Id = record.Id,
            Name = record.Name
        });
    }
}