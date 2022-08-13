using System.ComponentModel.DataAnnotations;
using MediatR;
using Svd.Backend.Application.Responses;
using Svd.Backend.Domain.Enums;

namespace Svd.Backend.Application.Features.Bags.Commands.CreateBag;

public class CreateParcelBagCommand : IRequest<BaseResponse<BagViewModel>>
{
    [Required]
    public string BagNumber { get; set; }
    [Required]
    public BagType Type { get; set; }
    [Required]
    public Guid ShipmentId { get; set; }

    public CreateParcelBagCommand(string bagNumber, BagType type, Guid shipmentId)
    {
        BagNumber = bagNumber;
        Type = type;
        ShipmentId = shipmentId;
    }
}
