using Svd.Backend.Domain.Enums;

namespace Svd.Backend.Application.Features.Airports;

public class AirportViewModel
{
    public AirportViewModel(Guid airportId, Airport airport, string code)
    {
        AirportId = airportId;
        Airport = airport;
        Code = code;
    }

    public Guid AirportId { get; set; }
    public Airport Airport { get; set; }
    public string Code { get; set; }
}
