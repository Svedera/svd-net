using AutoMapper;
using MediatR;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Features.Airports.Queries.GetCountriesCommand;

public class GetAirportsCommandHandler : IRequestHandler<GetAirportsCommand, List<AirportViewModel>>
{
    private readonly IAsyncRepository<AirportDetails> _airportsRepository;
    private readonly IMapper _mapper;

    public GetAirportsCommandHandler(
        IMapper mapper,
        IAsyncRepository<AirportDetails> airportsRepository)
    {
        _airportsRepository = airportsRepository;
        _mapper = mapper;
    }

    public async Task<List<AirportViewModel>> Handle(
        GetAirportsCommand request,
        CancellationToken cancellationToken)
    {
        var allCountries = (
                await _airportsRepository.ListAllAsync())
            .OrderBy(airport => airport.Code);

        var mapped = _mapper.Map<List<AirportViewModel>>(allCountries);
        return mapped;
    }
}
