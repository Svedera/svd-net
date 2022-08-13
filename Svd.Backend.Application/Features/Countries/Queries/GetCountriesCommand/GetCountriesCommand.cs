using MediatR;

namespace Svd.Backend.Application.Features.Countries.Queries.GetCountriesCommand;

public class GetCountriesCommand : IRequest<List<CountryViewModel>>
{
}
