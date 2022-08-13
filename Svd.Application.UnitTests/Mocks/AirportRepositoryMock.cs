using Moq;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Domain.Entities;
using Svd.Backend.Persistence.Defaults;

namespace Svd.Application.UnitTests.Mocks;

public static class AirportRepositoryMock
{
    public static Mock<IAsyncRepository<AirportDetails>> GetAirportRepository()
    {
        var airportDetailsList = AirportDefaults.Airports.ToList();

        var mockShipmentRepository = new Mock<IAsyncRepository<AirportDetails>>();
        mockShipmentRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(airportDetailsList);
        mockShipmentRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
            (Guid id) =>
            {
                var searchedAirport = airportDetailsList.FirstOrDefault(airport => airport.AirportId == id);
                return searchedAirport;
            });

        return mockShipmentRepository;
    }
}
