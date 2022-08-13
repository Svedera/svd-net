using FluentValidation;
using Svd.Backend.Application.Configurations;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Domain.Enums;

namespace Svd.Backend.Application.Features.Bags.Commands.CreateBag;

public class CreateParcelBagCommandValidator :
    AbstractValidator<CreateParcelBagCommand>
{
    private readonly IBagRepository _bagRepository;
    private readonly IShipmentRepository _shipmentRepository;

    public CreateParcelBagCommandValidator(
        IBagRepository bagRepository,
        IShipmentRepository shipmentRepository,
        ShipmentSettings settings)
    {
        _bagRepository = bagRepository;
        _shipmentRepository = shipmentRepository;

        RuleFor(bag => bag.Type)
            .Equal(BagType.Parcel)
            .WithMessage("Invalid bag type. Should be a letter bag.");

        var errorMessage = "{PropertyName} does not match the format: " + settings.BagNumberFormat;
        RuleFor(bag => bag.BagNumber)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Matches(settings.BagNumberFormat)
            .WithMessage(errorMessage)
            .MustAsync(BagNumberUnique)
            .WithMessage("A bag with the same bag number already exists.");

        RuleFor(bag => bag.ShipmentId)
            .MustAsync(ShipmentExists).WithMessage("Specified shipment does not exist.")
            .MustAsync(ShipmentNotFinalized).WithMessage("Shipment is already finalized.");
    }

    private async Task<bool> BagNumberUnique(
        string? bagNumber,
        CancellationToken token)
    {
        if (bagNumber == null) throw new ArgumentNullException(nameof(bagNumber));

        var exists = await _bagRepository
            .BagNumberExists(bagNumber);
        return !exists;
    }

    private async Task<bool> ShipmentExists(
        Guid guid,
        CancellationToken token)
    {
        var shipment = await _shipmentRepository
            .GetByIdAsync(guid);
        return shipment != null;
    }

    private async Task<bool> ShipmentNotFinalized(
        Guid guid,
        CancellationToken token)
    {
        var finalized = await _shipmentRepository
            .IsShipmentFinalized(guid);
        return !finalized;
    }
}
