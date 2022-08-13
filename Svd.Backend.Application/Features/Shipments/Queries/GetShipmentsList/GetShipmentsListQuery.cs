using MediatR;

namespace Svd.Backend.Application.Features.Shipments.Queries.GetShipmentsList;

public class GetShipmentListQuery : IRequest<List<ShipmentViewModel>>
{
}
