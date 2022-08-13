using MediatR;

namespace Svd.Backend.Application.Features.Parcels.Queries.GetParcels;

public class GetParcelsQuery : IRequest<List<ParcelViewModel>>
{
}
