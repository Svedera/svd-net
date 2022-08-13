using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Svd.Backend.Application.Features.Bags.Queries.GetShipmentBagsList;

public class GetShipmentBagsListQuery : IRequest<List<BagViewModel>>
{
    [Required] public Guid ShipmentId { get; set; }

    public GetShipmentBagsListQuery(Guid shipmentId)
    {
        ShipmentId = shipmentId;
    }
}
