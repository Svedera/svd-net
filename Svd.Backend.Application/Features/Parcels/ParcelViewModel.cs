using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Features.Parcels;

public class ParcelViewModel
{
    public Guid ParcelId { get; set; }
    public string ParcelNumber { get; set; }
    public string RecipientName { get; set; }
    public double Weight { get; set; }
    public double Price { get; set; }

    public CountryDetails Country { get; set; }
    public Guid BagId { get; set; }

    public ParcelViewModel(
        Guid parcelId,
        string parcelNumber,
        string recipientName,
        double weight,
        double price,
        CountryDetails country,
        Guid bagId)
    {
        ParcelId = parcelId;
        ParcelNumber = parcelNumber;
        RecipientName = recipientName;
        Weight = weight;
        Price = price;
        Country = country;
        BagId = bagId;
    }
}
