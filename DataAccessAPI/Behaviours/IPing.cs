namespace DataAccessAPI.Behaviours;

public interface IPing
{
    public Task<bool> PingAsync(CancellationToken cancellationToken = default);
}