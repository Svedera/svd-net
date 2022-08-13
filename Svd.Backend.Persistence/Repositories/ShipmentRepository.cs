using Microsoft.EntityFrameworkCore;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Application.Exceptions;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Persistence.Repositories;

public class ShipmentRepository : BaseRepository<Shipment>, IShipmentRepository
{
    public ShipmentRepository(SvdDbContext dbContext) : base(dbContext)
    {
    }

    public new Task<Shipment?> GetByIdAsync(Guid id)
    {
        return DbContext
            .Shipments
            .Include(shipment => shipment.Airport)
            .Include(shipment => shipment.Bags)
            .FirstOrDefaultAsync(shipment => shipment.ShipmentId == id);
    }

    public new async Task<IReadOnlyList<Shipment>> ListAllAsync()
    {
        return await DbContext
            .Shipments
            .Include(shipment => shipment.Airport)
            .Include(shipment => shipment.Bags)
            .ToListAsync();
    }

    public Task<bool> ShipmentNumberExists(string shipmentNumber)
    {
        var matches = DbContext
            .Shipments
            .AnyAsync(shipment => shipment.ShipmentNumber.Equals(shipmentNumber));
        return matches;
    }

    public async Task<bool> IsShipmentFinalized(Guid shipmentId)
    {
        var shipment = await DbContext
            .Shipments
            .FirstOrDefaultAsync(shipment =>
                shipment.ShipmentId.Equals(shipmentId));

        if (shipment == null)
        {
            throw new NotFoundException(nameof(shipment), shipmentId);
        }

        return shipment.Finalized;
    }
}
