using MediatR;
using Microsoft.AspNetCore.Mvc;
using Svd.Backend.Application.Features.Bags;
using Svd.Backend.Application.Features.Bags.Commands.CreateBag;
using Svd.Backend.Application.Features.Bags.Commands.CreateLetterBag;
using Svd.Backend.Application.Features.Bags.Queries.GetBagsList;
using Svd.Backend.Application.Responses;

namespace Svd.Backend.PostOffice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BagsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BagsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetAllBags")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<BagViewModel>>> GetAllBags()
    {
        var bags = await _mediator.Send(new GetBagsListQuery());
        return Ok(bags);
    }

    [HttpPost("Parcel", Name = "AddParcelBag")]
    public async Task<ActionResult<BaseResponse<BagViewModel>>> CreateParcelBag(
        [FromBody] CreateParcelBagCommand createParcelBagCommand)
    {
        var bag = await _mediator.Send(createParcelBagCommand);
        return Ok(bag);
    }

    [HttpPost("Letter", Name = "AddLetterBag")]
    public async Task<ActionResult<BaseResponse<BagViewModel>>> CreateLetterBag(
        [FromBody] CreateLetterBagCommand createBagCommand)
    {
        var bag = await _mediator.Send(createBagCommand);
        return Ok(bag);
    }
}
