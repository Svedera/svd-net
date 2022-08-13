using AutoMapper;
using MediatR;
using Svd.Backend.Application.Contracts.Persistence;

namespace Svd.Backend.Application.Features.Bags.Queries.GetShipmentBagsList;

public class GetShipmentBagsListQueryHandler : IRequestHandler<GetShipmentBagsListQuery, List<BagViewModel>>
{
    private readonly IBagRepository _bagRepository;
    private readonly IMapper _mapper;

    public GetShipmentBagsListQueryHandler(
        IMapper mapper,
        IBagRepository bagRepository)
    {
        _bagRepository = bagRepository;
        _mapper = mapper;
    }

    public async Task<List<BagViewModel>> Handle(
        GetShipmentBagsListQuery request,
        CancellationToken cancellationToken)
    {
        var allShipments = (
                await _bagRepository.ShipmentBagsListAsync(request.ShipmentId))
            .OrderBy(shipment => shipment.CreatedDate);

        var mappedBag = _mapper.Map<List<BagViewModel>>(allShipments);
        return mappedBag;
    }
}
