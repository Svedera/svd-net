using AutoMapper;
using Moq;
using Shouldly;
using Svd.Application.UnitTests.Mocks;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Application.Features.Shipments;
using Svd.Backend.Application.Features.Shipments.Queries.GetShipmentsList;
using Svd.Backend.Application.Profiles;

namespace Svd.Application.UnitTests.Shipments.Queries;

public class GetShipmentsListQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IShipmentRepository> _mockShipmentRepository;

    public GetShipmentsListQueryHandlerTests()
    {
        _mockShipmentRepository = RepositoryMocks.GetShipmenRepository();
        var configurationProvider = new MapperConfiguration(
            cfg => { cfg.AddProfile<MappingProfile>(); });

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task GetShipmentsListTest()
    {
        var handler = new GetShipmentsListQueryHandler(
            _mapper,
            _mockShipmentRepository.Object);

        var result = await handler.Handle(
            new GetShipmentListQuery(),
            CancellationToken.None);

        result.ShouldBeOfType<List<ShipmentViewModel>>();

        result.Count.ShouldBe(2);
    }
}
