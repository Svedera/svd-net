using MediatR;
using Microsoft.AspNetCore.Mvc;
using Svd.Backend.Application.Features.Parcels;
using Svd.Backend.Application.Features.Parcels.Commands.CreateParcel;
using Svd.Backend.Application.Features.Parcels.Queries.GetParcel;
using Svd.Backend.Application.Features.Parcels.Queries.GetParcels;
using Svd.Backend.Application.Responses;

namespace Svd.Backend.PostOffice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParcelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ParcelsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}", Name = "GetParcel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<ParcelViewModel>>> GetParcel(Guid id)
    {
        var parcel = await _mediator.Send(new GetParcelQuery(id));
        return Ok(parcel);
    }

    [HttpGet(Name = "GetParcels")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<ParcelViewModel>>> GetParcels()
    {
        var parcels = await _mediator.Send(new GetParcelsQuery());

        return Ok(parcels);
    }

    [HttpPost(Name = "AddParcel")]
    public async Task<ActionResult<BaseResponse<ParcelViewModel>>> Create(
        [FromBody] CreateParcelCommand createParcelCommand)
    {
        var parcel = await _mediator.Send(createParcelCommand);
        return Ok(parcel);
    }
}
