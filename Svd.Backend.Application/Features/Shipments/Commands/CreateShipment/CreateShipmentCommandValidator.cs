using FluentValidation;
using Svd.Backend.Application.Configurations;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Features.Shipments.Commands.CreateShipment;

public class CreateShipmentCommandValidator : AbstractValidator<CreateShipmentCommand>
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IAsyncRepository<AirportDetails> _airportRepository;

    public CreateShipmentCommandValidator(
        IShipmentRepository shipmentRepository,
        IAsyncRepository<AirportDetails> airportRepository,
        ShipmentSettings settings)
    {
        _shipmentRepository = shipmentRepository;
        _airportRepository = airportRepository;

        RuleFor(shipment => shipment.ShipmentNumber)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Matches(settings.ShipmentNumberFormat)
            .WithMessage("{PropertyName} does not match the format: " + settings.ShipmentNumberFormat)
            .MustAsync(ShipmentNumberUnique)
            .WithMessage("A shipment with the same shipment number already exists.");

        RuleFor(shipment => shipment.FlightNumber)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Matches(settings.FlightNumberFormat)
            .WithMessage("{PropertyName} does not match the format.");

        RuleFor(shipment => shipment.FlightDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(shipment => shipment.AirportId)
            .MustAsync(AirportExists)
            .WithMessage("Specified airport does not exist.");
    }

    private async Task<bool> ShipmentNumberUnique(
        string shipmentNumber,
        CancellationToken token)
    {
        var exists = await _shipmentRepository
            .ShipmentNumberExists(shipmentNumber);
        return !exists;
    }

    private async Task<bool> AirportExists(
        Guid countryId,
        CancellationToken token)
    {
        var airport = await _airportRepository
            .GetByIdAsync(countryId);
        return airport is not null;
    }
}
