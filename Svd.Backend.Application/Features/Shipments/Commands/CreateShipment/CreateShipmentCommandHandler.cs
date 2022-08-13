using AutoMapper;
using MediatR;
using Svd.Backend.Application.Configurations;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Application.Exceptions;
using Svd.Backend.Application.Responses;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Features.Shipments.Commands.CreateShipment;

public class CreateShipmentCommandHandler :
    IRequestHandler<CreateShipmentCommand, BaseResponse<ShipmentViewModel>>
{
    private readonly IAsyncRepository<AirportDetails> _airportRepository;
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IMapper _mapper;
    private readonly ShipmentSettings _shipmentSettings;

    public CreateShipmentCommandHandler(
        IMapper mapper,
        IShipmentRepository shipmentRepository,
        IAsyncRepository<AirportDetails> airportRepository,
        ShipmentSettings shipmentSettings)
    {
        _mapper = mapper;
        _shipmentRepository = shipmentRepository;
        _shipmentSettings = shipmentSettings;
        _airportRepository = airportRepository;
    }

    public async Task<BaseResponse<ShipmentViewModel>> Handle(
        CreateShipmentCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateShipmentCommandValidator(
            _shipmentRepository,
            _airportRepository,
            _shipmentSettings);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

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

        var shipment = _mapper.Map<Shipment>(request);

        var airport = await _airportRepository.GetByIdAsync(request.AirportId);
        if (airport == null)
        {
            throw new NotFoundException(nameof(CountryDetails), request.AirportId);
        }
        shipment.Airport = airport;

        shipment = await _shipmentRepository.AddAsync(shipment);
        var shipmentModel = _mapper.Map<ShipmentViewModel>(shipment);

        var response = new BaseResponse<ShipmentViewModel>(shipmentModel);
        return response;
    }
}
