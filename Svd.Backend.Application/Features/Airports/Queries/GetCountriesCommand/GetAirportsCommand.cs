using MediatR;

namespace Svd.Backend.Application.Features.Airports.Queries.GetCountriesCommand;

public class GetAirportsCommand : IRequest<List<AirportViewModel>>
{

}
