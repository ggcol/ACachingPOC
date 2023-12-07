using System.Text.Json;
using Shared.Entities;
using Shared.Responses.Programmers;

namespace DataAccessAPI;

public interface IDataGateway<T>
{
    public Task<IEnumerable<T>> GetAllAsync(
        CancellationToken cancellationToken = default);
}

public sealed class ProgrammersGateway<T> : IDataGateway<T> where T : Programmer
{
    private const string _baseUrl = "https://localhost:7107";
    private const string _getEnpoint = "Get";

    //this should normally be injected
    private HttpClient _client = new HttpClient();

    public async Task<IEnumerable<T>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _client.GetAsync(new Uri(_baseUrl + _getEnpoint),
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var deserialized =
            JsonSerializer.Deserialize<GetResponse<T>>(
                await response.Content.ReadAsStreamAsync(cancellationToken));

        if (deserialized is null)
            throw new NullReferenceException("Unable to deserialize");

        return deserialized.Programmers is not null &&
               deserialized.Programmers.Any()
            ? deserialized.Programmers
            : new List<T>();
    }
}