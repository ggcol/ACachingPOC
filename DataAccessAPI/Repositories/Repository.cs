using System.Text.Json;

namespace DataAccessAPI.Repositories;

public abstract class Repository<T>
{
    private readonly string _baseUrl;

    protected Repository(string baseUrl)
    {
        _baseUrl = baseUrl;
    }
    
    protected static async Task<T[]?> GetContent(
        HttpResponseMessage response, CancellationToken cancellationToken)
    {
        return await JsonSerializer.DeserializeAsync<T[]>(
            await response.Content.ReadAsStreamAsync(cancellationToken),
            cancellationToken: cancellationToken);
    }
    
    protected Uri MakeUri(string endpoint)
    {
        return new Uri(_baseUrl + endpoint);
    }
}