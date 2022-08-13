using AutoMapper;
using MediatR;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Application.Exceptions;
using Svd.Backend.Application.Responses;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Features.Shipments.Commands.FinalizeShipment;

public class FinalizeShipmentCommandHandler :
    IRequestHandler<FinalizeShipmentCommand, BaseResponse<ShipmentViewModel>>
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IMapper _mapper;

    public FinalizeShipmentCommandHandler(
        IShipmentRepository shipmentRepository,
        IMapper mapper)
    {
        _shipmentRepository = shipmentRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ShipmentViewModel>> Handle(
        FinalizeShipmentCommand request,
        CancellationToken cancellationToken)
    {
        var shipmentToFinalize = await _shipmentRepository.GetByIdAsync(request.ShipmentId);

        if (shipmentToFinalize == null)
        {
            throw new NotFoundException(nameof(Shipment), request.ShipmentId);
        }

        var validator = new FinalizeShipmentValidator();
        var validationResult = await validator.ValidateAsync(shipmentToFinalize, cancellationToken);

        if (!validationResult.IsValid)
        {
            const string message = "Validation failed.";
            var invalidResponse =
                new BaseResponse<ShipmentViewModel>(false, message);

            foreach (var error in validationResult.Errors)
            {
                invalidResponse
                    .ValidationErrors
                    .Add(error.ErrorMessage);
            }
            return invalidResponse;
        }

        shipmentToFinalize.Finalized = true;

        await _shipmentRepository.UpdateAsync(shipmentToFinalize);

        var updatedShipment = _mapper.Map<ShipmentViewModel>(shipmentToFinalize);
        var response = new BaseResponse<ShipmentViewModel>(updatedShipment);
        return response;
    }
}
