using AutoMapper;
using MediatR;
using Svd.Backend.Application.Contracts.Persistence;

namespace Svd.Backend.Application.Features.Parcels.Queries.GetParcel;

public class GetParcelQueryHandler : IRequestHandler<GetParcelQuery, ParcelViewModel>
{
    private readonly IParcelRepository _parcelRepository;
    private readonly IMapper _mapper;

    public GetParcelQueryHandler(
        IMapper mapper,
        IParcelRepository parcelRepository)
    {
        _parcelRepository = parcelRepository;
        _mapper = mapper;
    }

    public async Task<ParcelViewModel> Handle(
        GetParcelQuery request,
        CancellationToken cancellationToken)
    {
        var parcel = await _parcelRepository.GetByIdAsync(request.ParcelId);

        var mappedParcel = _mapper.Map<ParcelViewModel>(parcel);
        return mappedParcel;
    }
}
