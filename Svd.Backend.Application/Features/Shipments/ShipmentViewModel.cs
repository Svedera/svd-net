using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Features.Shipments;

public class ShipmentViewModel
{
    public Guid ShipmentId { get; set; }
    public string ShipmentNumber { get; set; }
    public string FlightNumber { get; set; }
    public DateTime FlightDate { get; set; }
    public bool Finalized { get; set; }
    public AirportDetails Airport { get; set; }
    public int BagCount { get; set; }

    public ShipmentViewModel(
        Guid shipmentId,
        string shipmentNumber,
        string flightNumber,
        DateTime flightDate,
        bool finalized,
        AirportDetails airport)
    {
        ShipmentId = shipmentId;
        ShipmentNumber = shipmentNumber;
        FlightNumber = flightNumber;
        FlightDate = flightDate;
        Finalized = finalized;
        Airport = airport;
    }
}
