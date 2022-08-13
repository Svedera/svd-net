using AutoMapper;
using MediatR;
using Svd.Backend.Application.Contracts.Persistence;

namespace Svd.Backend.Application.Features.Shipments.Queries.GetShipmentsList;

public class GetShipmentsListQueryHandler : IRequestHandler<GetShipmentListQuery, List<ShipmentViewModel>>
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IMapper _mapper;

    public GetShipmentsListQueryHandler(
        IMapper mapper,
        IShipmentRepository shipmentRepository)
    {
        _shipmentRepository = shipmentRepository;
        _mapper = mapper;
    }

    public async Task<List<ShipmentViewModel>> Handle(
        GetShipmentListQuery request,
        CancellationToken cancellationToken)
    {
        var allShipments = (
                await _shipmentRepository.ListAllAsync())
            .OrderBy(shipment => shipment.CreatedDate);

        var mappedShipment = _mapper.Map<List<ShipmentViewModel>>(allShipments);
        return mappedShipment;
    }
}
