using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Contracts.Persistence;

public interface IShipmentRepository : IAsyncRepository<Shipment>
{
    Task<bool> ShipmentNumberExists(string name);
    Task<bool> IsShipmentFinalized(Guid guid);
}
