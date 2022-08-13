using AutoMapper;
using Moq;
using Shouldly;
using Svd.Application.UnitTests.Mocks;
using Svd.Backend.Application.Configurations;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Application.Features.Shipments.Commands.CreateShipment;
using Svd.Backend.Application.Profiles;
using Svd.Backend.Domain.Entities;
using Svd.Backend.Persistence.Defaults;

namespace Svd.Application.UnitTests.Shipments.Commands;

public class CreateShipmentTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IShipmentRepository> _mockShipmentRepository;
    private readonly Mock<IAsyncRepository<AirportDetails>> _mockAirportRepository;
    private readonly ShipmentSettings _shipmentSettings;


    public CreateShipmentTests()
    {
        _mockShipmentRepository = RepositoryMocks.GetShipmenRepository();
        _mockAirportRepository = AirportRepositoryMock.GetAirportRepository();
        _shipmentSettings = SettingsMocks.GetShipmentSettingsMock();

        var configurationProvider = new MapperConfiguration(
            config => { config.AddProfile<MappingProfile>(); });

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task Handle_ValidCategory_AddedToCategoriesRepo()
    {
        var shipmentsCount = (await _mockShipmentRepository
            .Object
            .ListAllAsync()).Count;

        var handler = new CreateShipmentCommandHandler(
            _mapper,
            _mockShipmentRepository.Object,
            _mockAirportRepository.Object,
            _shipmentSettings
        );
        const string shipmentNumber = "111-aaaaaz";
        const string flightNumber = "aa1111";
        var flightDate = DateTime.Now;
        var airportId = AirportDefaults.Airports.FirstOrDefault()!.AirportId;

        var command = new CreateShipmentCommand(
            shipmentNumber,
            flightNumber,
            flightDate,
            airportId);
        await handler.Handle(command, CancellationToken.None);

        var allShipments = await _mockShipmentRepository.Object.ListAllAsync();
        allShipments.Count.ShouldBe(shipmentsCount + 1);
    }
}
