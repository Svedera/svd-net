using Moq;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Domain.Entities;
using Svd.Backend.Domain.Enums;
using Svd.Backend.Persistence.Defaults;

namespace Svd.Application.UnitTests.Mocks;

public static class RepositoryMocks
{
    static readonly Guid ShipmentIdTll;
    static readonly Guid ShipmentIdRix;

    static RepositoryMocks()
    {
        ShipmentIdRix = Guid.Parse("{d9eef48f-edf6-4470-aa05-08da7fb72943}");
        ShipmentIdTll = Guid.Parse("{daf1f6f3-9137-43e0-aa04-08da7fb72943}");
    }

    public static Mock<IShipmentRepository> GetShipmenRepository()
    {
        var tllAirport = AirportDefaults.Airports
            .FirstOrDefault(airport => airport.Airport == Airport.Tll);
        var rixAirport = AirportDefaults.Airports
            .FirstOrDefault(airport => airport.Airport == Airport.Rix);

        var shipments = new List<Shipment>
        {
            new()
            {
                ShipmentId = ShipmentIdTll,
                ShipmentNumber = "111-aaaaab",
                FlightNumber = "aa1111",
                FlightDate = DateTime.Now,
                Finalized = true,
                Airport = tllAirport!
            },
            new()
            {
                ShipmentId = ShipmentIdRix,
                ShipmentNumber = "111-aaaaab",
                FlightNumber = "aa1111",
                FlightDate = DateTime.Now,
                Finalized = true,
                Airport = rixAirport!
            }
        };

        var mockShipmentRepository = new Mock<IShipmentRepository>();
        mockShipmentRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(shipments);
        mockShipmentRepository.Setup(repo => repo.AddAsync(It.IsAny<Shipment>())).ReturnsAsync(
            (Shipment shipment) =>
            {
                shipments.Add(shipment);
                return shipment;
            });

        return mockShipmentRepository;
    }
}
