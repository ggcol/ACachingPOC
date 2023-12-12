using Shared;

namespace DataAccessAPI.Repositories.Health;

public sealed class HealthRepository : Repository<string>, IHealthRepository
{
    private readonly HttpClient _client;

    public HealthRepository(HttpClient client, string baseUrl) : base(baseUrl)
    {
        _client = client;
    }

    public async Task<bool> PingAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _client
                .GetAsync(MakeUri(Routes.Health.Ping), cancellationToken)
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            return false;
        }
    }
}