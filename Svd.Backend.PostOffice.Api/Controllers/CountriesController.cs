using MediatR;
using Microsoft.AspNetCore.Mvc;
using Svd.Backend.Application.Features.Countries;
using Svd.Backend.Application.Features.Countries.Queries.GetCountriesCommand;

namespace Svd.Backend.PostOffice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CountriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetAllCountries")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<CountryViewModel>>> GetAllBags()
    {
        var bags = await _mediator.Send(new GetCountriesCommand());
        return Ok(bags);
    }
}
