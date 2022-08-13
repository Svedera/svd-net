using AutoMapper;
using MediatR;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Features.Countries.Queries.GetCountriesCommand;

public class GetCountriesCommandHandler : IRequestHandler<GetCountriesCommand, List<CountryViewModel>>
{
    private readonly IAsyncRepository<CountryDetails> _countyRepository;
    private readonly IMapper _mapper;

    public GetCountriesCommandHandler(
        IMapper mapper,
        IAsyncRepository<CountryDetails> countyRepository)
    {
        _countyRepository = countyRepository;
        _mapper = mapper;
    }

    public async Task<List<CountryViewModel>> Handle(
        GetCountriesCommand request,
        CancellationToken cancellationToken)
    {
        var allCountries = (
                await _countyRepository.ListAllAsync())
            .OrderBy(countryDetails => countryDetails.Code);

        var mapped = _mapper.Map<List<CountryViewModel>>(allCountries);
        return mapped;
    }
}
