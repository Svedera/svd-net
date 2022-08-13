using System.ComponentModel.DataAnnotations;
using MediatR;
using Svd.Backend.Application.Responses;

namespace Svd.Backend.Application.Features.Shipments.Commands.CreateShipment;

public class CreateShipmentCommand : IRequest<BaseResponse<ShipmentViewModel>>
{
    [Required] public string ShipmentNumber { get; set; }
    [Required] public string FlightNumber { get; set; }
    [Required] public DateTime FlightDate { get; set; }
    [Required] public Guid AirportId { get; set; }

    public CreateShipmentCommand(
        string shipmentNumber,
        string flightNumber,
        DateTime flightDate,
        Guid airportId)
    {
        ShipmentNumber = shipmentNumber;
        FlightNumber = flightNumber;
        FlightDate = flightDate;
        AirportId = airportId;
    }

    public override string ToString()
    {
        var shipment = $"Shipment number: {ShipmentNumber};" +
                       $" flight number: {FlightNumber};" +
                       $" Flight date: {FlightDate};" +
                       $" AirportId: {AirportId}";

        return shipment;
    }
}
