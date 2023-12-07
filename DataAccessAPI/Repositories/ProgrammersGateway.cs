using System.Text.Json;
using Shared.Entities;

namespace DataAccessAPI.Repositories;

public sealed class ProgrammersGateway : IDataGateway<Programmer>
{
    private const string _baseUrl = "https://localhost:7107/";
    private const string _getAllEnpoint = "GetAll";

    //this should normally be injected
    private readonly HttpClient _client = new HttpClient();

    public async Task<IReadOnlyList<Programmer>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _client.GetAsync(MakeUri(_getAllEnpoint), 
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var deserialized = await GetContent(response, cancellationToken);

        if (deserialized is null)
            throw new NullReferenceException("Unable to deserialize");

        return deserialized.Any()
            ? deserialized
            : new List<Programmer>();
    }

    //TODO abstract this
    private static async Task<Programmer[]?> GetContent(
        HttpResponseMessage response, CancellationToken cancellationToken)
    {
        return await JsonSerializer.DeserializeAsync<Programmer[]>(
            await response.Content.ReadAsStreamAsync(cancellationToken),
            cancellationToken: cancellationToken);
    }

    //TODO abstract this
    private static Uri MakeUri(string endpoint)
    {
        return new Uri(_baseUrl + endpoint);
    }
}