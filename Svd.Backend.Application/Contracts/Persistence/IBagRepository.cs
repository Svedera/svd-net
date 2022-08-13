using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Contracts.Persistence;

public interface IBagRepository : IAsyncRepository<Bag>
{
    Task<bool> IsBagShipmentFinalized(Guid guid);
    Task<bool> BagNumberExists(string name);
    Task<IReadOnlyList<Bag>> ShipmentBagsListAsync(Guid shipmentId);
}
