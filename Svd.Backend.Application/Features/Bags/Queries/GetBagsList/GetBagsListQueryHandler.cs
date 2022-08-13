using AutoMapper;
using MediatR;
using Svd.Backend.Application.Contracts.Persistence;

namespace Svd.Backend.Application.Features.Bags.Queries.GetBagsList;

public class GetBagsListQueryHandler : IRequestHandler<GetBagsListQuery, List<BagViewModel>>
{
    private readonly IBagRepository _bagRepository;
    private readonly IMapper _mapper;

    public GetBagsListQueryHandler(
        IMapper mapper,
        IBagRepository bagRepository)
    {
        _bagRepository = bagRepository;
        _mapper = mapper;
    }

    public async Task<List<BagViewModel>> Handle(
        GetBagsListQuery request,
        CancellationToken cancellationToken)
    {
        var allShipments = (
                await _bagRepository.ListAllAsync())
            .OrderBy(shipment => shipment.CreatedDate);

        var mappedBag = _mapper.Map<List<BagViewModel>>(allShipments);
        return mappedBag;
    }
}
