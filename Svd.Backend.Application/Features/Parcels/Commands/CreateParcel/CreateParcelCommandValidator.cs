using FluentValidation;
using Svd.Backend.Application.Configurations;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Domain.Entities;
using Svd.Backend.Domain.Enums;

namespace Svd.Backend.Application.Features.Parcels.Commands.CreateParcel;

public class CreateParcelCommandValidator : AbstractValidator<CreateParcelCommand>
{
    private readonly IAsyncRepository<CountryDetails> _countryRepository;
    private readonly IParcelRepository _parcelRepository;
    private readonly IBagRepository _bagRepository;

    public CreateParcelCommandValidator(
        IParcelRepository parcelRepository,
        IBagRepository bagRepository,
        IAsyncRepository<CountryDetails> countryRepository,
        ShipmentSettings settings)
    {
        _parcelRepository = parcelRepository;
        _bagRepository = bagRepository;
        _countryRepository = countryRepository;

        RuleFor(parcel => parcel.ParcelNumber)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Matches(settings.ParcelNumberFormat)
            .WithMessage("{PropertyName} does not match the format: " + settings.ParcelNumberFormat)
            .MustAsync(ParcelNumberUnique)
            .WithMessage("A parcel with the same parcel number already exists.");

        RuleFor(parcel => parcel.RecipientName)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(parcel => parcel.RecipientName)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(parcel => parcel.BagId)
            .MustAsync(ParcelBagExists).WithMessage("Specified parcel bag does not exist.")
            .MustAsync(ShipmentNotFinalized).WithMessage("Shipment is already finalized.");

        RuleFor(parcel => parcel.CountryId)
            .MustAsync(CountryExists).WithMessage("Specified country does not exist.");

        RuleFor(parcel => parcel.Price)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} can not be negative.");

        RuleFor(parcel => parcel.Weight)
            .GreaterThan(0).WithMessage("{PropertyName} can not be negative.");
    }

    private async Task<bool> CountryExists(
        Guid countryId,
        CancellationToken token)
    {
        var county = await _countryRepository
            .GetByIdAsync(countryId);
        return county is not null;
    }

    private async Task<bool> ParcelNumberUnique(
        string parcelNumber,
        CancellationToken token)
    {
        var exists = await _parcelRepository
            .ParcelNumberExists(parcelNumber);
        return !exists;
    }

    private async Task<bool> ParcelBagExists(
        Guid guid,
        CancellationToken token)
    {
        var bag = await _bagRepository.GetByIdAsync(guid);
        return bag is { Type: BagType.Parcel };
    }

    private async Task<bool> ShipmentNotFinalized(
        Guid guid,
        CancellationToken token)
    {
        var finalized = await _bagRepository.IsBagShipmentFinalized(guid);
        return !finalized;
    }
}
