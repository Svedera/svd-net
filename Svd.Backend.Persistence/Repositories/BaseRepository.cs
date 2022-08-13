using Microsoft.EntityFrameworkCore;
using Svd.Backend.Application.Contracts.Persistence;

namespace Svd.Backend.Persistence.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    protected readonly SvdDbContext DbContext;

    public BaseRepository(SvdDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await DbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
        await DbContext.SaveChangesAsync();

        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }
}
