namespace DataAccessAPI;

public interface IDataGateway<T>
{
    public Task<IReadOnlyList<T>> GetAllAsync(
        CancellationToken cancellationToken = default);
}