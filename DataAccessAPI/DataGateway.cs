using DataAccessAPI.Repositories;
using DataAccessAPI.Repositories.Health;
using DataAccessAPI.Repositories.Programmer;

namespace DataAccessAPI;

public sealed class DataGateway : IDataGateway
{
    private readonly RepositoryCollector _repositoryCollector;
    private const string _baseUrl = "https://localhost:7107/";

    public IHealthRepository Health => _repositoryCollector.Health;

    public IProgrammersRepository Programmers =>
        _repositoryCollector.Programmers;

    public DataGateway(HttpClient client)
    {
        _repositoryCollector = new RepositoryCollector(
            new HealthRepository(client, _baseUrl),
            new ProgrammersRepository(client, _baseUrl));
    }
}