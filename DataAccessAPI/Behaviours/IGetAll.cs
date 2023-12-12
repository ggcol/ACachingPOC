namespace DataAccessAPI.Behaviours;

public interface IGetAll<T>
{
    public Task<IReadOnlyList<T>> GetAllAsync(
        CancellationToken cancellationToken = default);
}