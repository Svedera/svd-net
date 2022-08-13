using System.ComponentModel.DataAnnotations;
using MediatR;
using Svd.Backend.Application.Responses;

namespace Svd.Backend.Application.Features.Parcels.Commands.CreateParcel;

public class CreateParcelCommand : IRequest<BaseResponse<ParcelViewModel>>
{
    [Required] public string ParcelNumber { get; set; }
    [Required] public string RecipientName { get; set; }
    [Required] public double Weight { get; set; }
    [Required] public double Price { get; set; }

    [Required] public Guid CountryId { get; set; }
    [Required] public Guid BagId { get; set; }

    public CreateParcelCommand(
        string parcelNumber,
        string recipientName,
        double weight,
        double price,
        Guid countryId,
        Guid bagId)
    {
        ParcelNumber = parcelNumber;
        RecipientName = recipientName;
        Weight = weight;
        Price = price;
        CountryId = countryId;
        BagId = bagId;
    }
}
