using FluentValidation;
using Svd.Backend.Application.Configurations;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Domain.Enums;

namespace Svd.Backend.Application.Features.Bags.Commands.CreateLetterBag;

public class CreateLetterBagCommandValidator : AbstractValidator<CreateLetterBagCommand>
{
    private readonly IBagRepository _bagRepository;
    private readonly IShipmentRepository _shipmentRepository;

    public CreateLetterBagCommandValidator(
        IBagRepository bagRepository,
        IShipmentRepository shipmentRepository,
        ShipmentSettings settings)
    {
        _bagRepository = bagRepository;
        _shipmentRepository = shipmentRepository;

        RuleFor(bag => bag.Type)
            .Equal(BagType.Letter)
            .WithMessage("Invalid bag type. Should be a letter bag.");

        RuleFor(bag => bag.BagNumber)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Matches(settings.BagNumberFormat)
            .WithMessage("{PropertyName} does not match the format: " + settings.BagNumberFormat)
            .MustAsync(BagNumberUnique)
            .WithMessage("A bag with the same bag number already exists.");

        RuleFor(bag => bag.ShipmentId)
            .MustAsync(ShipmentExists).WithMessage("Specified shipment does not exist.");

        RuleFor(bag => bag.ItemsCount)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} can not be negative.");

        RuleFor(bag => bag.Price)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} can not be negative.");

        RuleFor(bag => bag.Weight)
            .GreaterThan(0).WithMessage("{PropertyName} can not be negative.");
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
}
