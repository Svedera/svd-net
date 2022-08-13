using MediatR;
using Microsoft.AspNetCore.Mvc;
using Svd.Backend.Application.Features.Airports;
using Svd.Backend.Application.Features.Airports.Queries.GetCountriesCommand;

namespace Svd.Backend.PostOffice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AirportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetAllAirports")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<AirportViewModel>>> GetAllBags()
    {
        var bags = await _mediator.Send(new GetAirportsCommand());
        return Ok(bags);
    }
}
