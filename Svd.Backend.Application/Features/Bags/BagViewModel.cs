using Svd.Backend.Domain.Enums;

namespace Svd.Backend.Application.Features.Bags;

public class BagViewModel
{
    public Guid BagId { get; set; }
    public string BagNumber { get; set; }
    public BagType Type { get; set; }

    public int ItemsCount { get; set; }
    public double Weight { get; set; }
    public double Price { get; set; }

    public Guid ShipmentId { get; set; }

    public BagViewModel(
        Guid bagId,
        string bagNumber,
        BagType type,
        int itemsCount,
        double weight,
        double price,
        Guid shipmentId)
    {
        BagId = bagId;
        BagNumber = bagNumber;
        Type = type;
        ItemsCount = itemsCount;
        Weight = weight;
        Price = price;
        ShipmentId = shipmentId;
    }
}
