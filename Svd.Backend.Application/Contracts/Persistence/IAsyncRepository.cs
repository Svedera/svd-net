namespace Svd.Backend.Application.Contracts.Persistence;

#pragma warning disable CS8618
public interface IAsyncRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
#pragma warning restore CS8618
