using System.ComponentModel.DataAnnotations;
using MediatR;
using Svd.Backend.Application.Responses;
using Svd.Backend.Domain.Enums;

namespace Svd.Backend.Application.Features.Bags.Commands.CreateLetterBag;

public class CreateLetterBagCommand : IRequest<BaseResponse<BagViewModel>>
{
    [Required] public string BagNumber { get; set; }
    [Required] public BagType Type { get; set; }
    [Required] public Guid ShipmentId { get; set; }
    [Required] public int ItemsCount { get; set; }
    [Required] public double Weight { get; set; }
    [Required] public double Price { get; set; }

    public CreateLetterBagCommand(
        string bagNumber,
        BagType type,
        Guid shipmentId,
        int itemsCount,
        double weight,
        double price)
    {
        BagNumber = bagNumber;
        Type = type;
        ShipmentId = shipmentId;
        ItemsCount = itemsCount;
        Weight = weight;
        Price = price;
    }
}
