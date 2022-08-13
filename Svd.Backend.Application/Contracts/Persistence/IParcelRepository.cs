using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Contracts.Persistence;

public interface IParcelRepository : IAsyncRepository<Parcel>
{
    Task<bool> ParcelNumberExists(string name);
}
