using Microsoft.EntityFrameworkCore;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Application.Exceptions;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Persistence.Repositories;

public class BagRepository : BaseRepository<Bag>, IBagRepository
{
    public BagRepository(SvdDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Bag>> ShipmentBagsListAsync(Guid shipmentId)
    {
        return await DbContext
            .Bags
            .Where(bag => bag.ShipmentId == shipmentId)
            .ToListAsync();
    }

    public Task<bool> BagNumberExists(string bagNumber)
    {
        var matches = DbContext
            .Bags
            .Any(bag => bag.BagNumber.Equals(bagNumber));

        return Task.FromResult(matches);
    }

    public async Task<bool> IsBagShipmentFinalized(Guid bagId)
    {
        var bag = await DbContext
            .Bags
            .Where(bag => bag.BagId == bagId)
            .FirstOrDefaultAsync();

        if (bag == null)
        {
            throw new NotFoundException(nameof(Bag), bagId);
        }

        var shipment = await DbContext
            .Shipments
            .FirstOrDefaultAsync(shipment =>
                shipment.ShipmentId.Equals(bag!.ShipmentId));

        if (shipment == null)
        {
            throw new NotFoundException(nameof(shipment), bag!.ShipmentId);
        }

        return shipment.Finalized;
    }
}
