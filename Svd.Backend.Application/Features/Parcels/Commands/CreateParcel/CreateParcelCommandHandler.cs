using AutoMapper;
using MediatR;
using Svd.Backend.Application.Configurations;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Application.Exceptions;
using Svd.Backend.Application.Responses;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Features.Parcels.Commands.CreateParcel;

public class CreateParcelCommandHandler :
    IRequestHandler<CreateParcelCommand, BaseResponse<ParcelViewModel>>
{
    private readonly IAsyncRepository<CountryDetails> _countryRepository;
    private readonly IParcelRepository _parcelRepository;
    private readonly IBagRepository _bagRepository;
    private readonly IMapper _mapper;
    private readonly ShipmentSettings _shipmentSettings;

    public CreateParcelCommandHandler(
        IMapper mapper,
        IParcelRepository parcelRepository,
        IBagRepository bagRepository,
        IAsyncRepository<CountryDetails> countryRepository,
        ShipmentSettings shipmentSettings)
    {
        _mapper = mapper;
        _parcelRepository = parcelRepository;
        _bagRepository = bagRepository;
        _shipmentSettings = shipmentSettings;
        _countryRepository = countryRepository;
    }

    public async Task<BaseResponse<ParcelViewModel>> Handle(
        CreateParcelCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateParcelCommandValidator(
            _parcelRepository,
            _bagRepository,
            _countryRepository,
            _shipmentSettings);
        var validationResult =
            await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            const string message = "Validation failed.";
            var invalidResponse =
                new BaseResponse<ParcelViewModel>(false, message);
            foreach (var error in validationResult.Errors)
            {
                invalidResponse
                    .ValidationErrors
                    .Add(error.ErrorMessage);
            }

            return invalidResponse;
        }

        var parcel = _mapper.Map<Parcel>(request);

        var country = await _countryRepository.GetByIdAsync(request.CountryId);
        if (country == null)
        {
            throw new NotFoundException(nameof(CountryDetails), request.CountryId);
        }

        parcel.Country = country;
        parcel = await _parcelRepository.AddAsync(parcel);
        var mappedParcel = _mapper.Map<ParcelViewModel>(parcel);

        var response = new BaseResponse<ParcelViewModel>(mappedParcel);
        return response;
    }
}
