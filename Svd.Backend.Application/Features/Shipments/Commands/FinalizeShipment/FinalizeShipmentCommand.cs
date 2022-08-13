using System.ComponentModel.DataAnnotations;
using MediatR;
using Svd.Backend.Application.Responses;

namespace Svd.Backend.Application.Features.Shipments.Commands.FinalizeShipment;

public class FinalizeShipmentCommand : IRequest<BaseResponse<ShipmentViewModel>>
{
    [Required]
    public Guid ShipmentId { get; }

    public FinalizeShipmentCommand(Guid shipmentId)
    {
        ShipmentId = shipmentId;
    }
}
