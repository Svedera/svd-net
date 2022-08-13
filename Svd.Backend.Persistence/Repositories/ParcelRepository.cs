using Microsoft.EntityFrameworkCore;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Application.Exceptions;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Persistence.Repositories;

public class ParcelRepository : BaseRepository<Parcel>, IParcelRepository
{
    public ParcelRepository(SvdDbContext dbContext) : base(dbContext)
    {
    }

    public new async Task<Parcel?> GetByIdAsync(Guid id)
    {
        return await DbContext.Parcels
            .Include(parcel => parcel.Country)
            .FirstOrDefaultAsync(parcel => parcel.ParcelId == id);
    }

    public new async Task<IReadOnlyList<Parcel>> ListAllAsync()
    {
        return await DbContext.Parcels
            .Include(parcel => parcel.Country)
            .ToListAsync();
    }

    public override async Task<Parcel> AddAsync(Parcel entity)
    {
        await DbContext.Set<Parcel>().AddAsync(entity);

        await UpdateParcelDetails(entity);

        await DbContext.SaveChangesAsync();

        return entity;
    }

    public new async Task UpdateAsync(Parcel entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;

        await UpdateParcelDetails(entity);

        await DbContext.SaveChangesAsync();
    }

    public new async Task DeleteAsync(Parcel entity)
    {
        DbContext.Set<Parcel>().Remove(entity);

        await UpdateParcelDetails(entity);

        await DbContext.SaveChangesAsync();
    }

    public Task<bool> ParcelNumberExists(string parcelNumber)
    {
        var matches = DbContext
            .Parcels
            .Any(parcel => parcel.ParcelNumber.Equals(parcelNumber));

        return Task.FromResult(matches);
    }

    private async Task UpdateParcelDetails(Parcel entity)
    {
        double weight = 0;
        double price = 0;
        int itemsCount = 0;

        var bag = await DbContext.Bags
            .FirstOrDefaultAsync(item => item.BagId == entity.BagId);

        if (bag is null)
        {
            throw new NotFoundException(nameof(Bag), entity.BagId);
        }

        var bagNotEmpty = DbContext
            .Parcels
            .Any(item => item.BagId == entity.BagId);
        if (bagNotEmpty)
        {
            var newValues = await DbContext
                .Parcels
                .Where(item => item.BagId == entity.BagId)
                .GroupBy(item => true)
                .Select(item => new
                {
                    Weight = item.Sum(p => p.Weight),
                    Price = item.Sum(p => p.Price),
                    ItemsCount = item.Count()
                })
                .FirstOrDefaultAsync();

            if (newValues == null)
            {
                throw new ApplicationException("Bag update query is invalid");
            }

            weight = newValues.Weight;
            price = newValues.Price;
            itemsCount = newValues.ItemsCount;
        }

        bag.Weight = weight + entity.Weight;
        bag.Price = price + entity.Price;
        bag.ItemsCount = itemsCount + 1;
    }
}
