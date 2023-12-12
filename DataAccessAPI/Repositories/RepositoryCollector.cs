using DataAccessAPI.Repositories.Health;
using DataAccessAPI.Repositories.Programmer;

namespace DataAccessAPI.Repositories;

internal sealed class RepositoryCollector : IDataGateway
{
    private readonly IHealthRepository? _health;
    private readonly IProgrammersRepository? _programmers;

    public IHealthRepository Health =>
        _health ?? throw new NullReferenceException();

    public IProgrammersRepository Programmers =>
        _programmers ?? throw new NullReferenceException();

    public RepositoryCollector(
        IHealthRepository? healthRepository = default,
        IProgrammersRepository? programmersRepository = default)
    {
        _health = healthRepository;
        _programmers = programmersRepository;
    }
}