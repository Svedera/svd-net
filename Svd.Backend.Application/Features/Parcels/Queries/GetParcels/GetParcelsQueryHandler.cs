using AutoMapper;
using MediatR;
using Svd.Backend.Application.Contracts.Persistence;

namespace Svd.Backend.Application.Features.Parcels.Queries.GetParcels;

public class GetParcelsQueryHandler : IRequestHandler<GetParcelsQuery, List<ParcelViewModel>>
{
    private readonly IParcelRepository _parcelRepository;
    private readonly IMapper _mapper;

    public GetParcelsQueryHandler(
        IMapper mapper,
        IParcelRepository parcelRepository)
    {
        _parcelRepository = parcelRepository;
        _mapper = mapper;
    }

    public async Task<List<ParcelViewModel>> Handle(
        GetParcelsQuery request,
        CancellationToken cancellationToken)
    {
        var parcel = await _parcelRepository.ListAllAsync();

        var mappedParcel = _mapper.Map<List<ParcelViewModel>>(parcel);
        return mappedParcel;
    }
}
