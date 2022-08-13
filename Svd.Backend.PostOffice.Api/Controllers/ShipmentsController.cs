using MediatR;
using Microsoft.AspNetCore.Mvc;
using Svd.Backend.Application.Features.Bags;
using Svd.Backend.Application.Features.Bags.Queries.GetShipmentBagsList;
using Svd.Backend.Application.Features.Shipments;
using Svd.Backend.Application.Features.Shipments.Commands.CreateShipment;
using Svd.Backend.Application.Features.Shipments.Commands.FinalizeShipment;
using Svd.Backend.Application.Features.Shipments.Queries.GetShipmentsList;
using Svd.Backend.Application.Responses;

namespace Svd.Backend.PostOffice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipmentsController : ControllerBase
{
    private readonly ILogger<ShipmentsController> _logger;
    private readonly IMediator _mediator;

    public ShipmentsController(
        IMediator mediator,
        ILogger<ShipmentsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet(Name = "GetAllShipments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<ShipmentViewModel>>> GetAllShipments()
    {
        var shipments = await _mediator.Send(new GetShipmentListQuery());
        return Ok(shipments);
    }

    [HttpGet("{shipmentId}/bags", Name = "GetBags")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<BagViewModel>>> GetShipmentBags(Guid shipmentId)
    {
        var bags = await _mediator.Send(new GetShipmentBagsListQuery(shipmentId));
        return Ok(bags);
    }

    [HttpPost(Name = "AddShipment")]
    public async Task<ActionResult<BaseResponse<ShipmentViewModel>>> Create(
        [FromBody] CreateShipmentCommand createEventCommand)
    {
        var shipment = await _mediator.Send(createEventCommand);
        return Ok(shipment);
    }

    [HttpPost("Finalize/{id}", Name = "FinalizeShipmentById")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponse<ShipmentViewModel>>> FinalizeShipmentById(Guid id)
    {
        var finalizeCommand = new FinalizeShipmentCommand(id);
        var shipment = await _mediator.Send(finalizeCommand);

        return Ok(shipment);
    }
}
