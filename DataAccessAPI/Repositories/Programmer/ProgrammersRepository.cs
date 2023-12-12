using Shared;

namespace DataAccessAPI.Repositories.Programmer;

public sealed class ProgrammersRepository
    : Repository<Shared.Entities.Programmer>, IProgrammersRepository
{
    private readonly HttpClient _client;

    public ProgrammersRepository(HttpClient client, string baseUrl)
        : base(baseUrl)
    {
        _client = client;
    }

    public async Task<IReadOnlyList<Shared.Entities.Programmer>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _client
            .GetAsync(MakeUri(Routes.Programmer.GetAll), cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var deserialized = await GetContent(response, cancellationToken);

        if (deserialized is null)
            throw new NullReferenceException("Unable to deserialize");

        return deserialized.Any()
            ? deserialized
            : new List<Shared.Entities.Programmer>();
    }
}