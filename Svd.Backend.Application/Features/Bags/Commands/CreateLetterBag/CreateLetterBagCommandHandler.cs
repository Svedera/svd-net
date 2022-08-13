using AutoMapper;
using MediatR;
using Svd.Backend.Application.Configurations;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Application.Responses;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Features.Bags.Commands.CreateLetterBag;

public class CreateLetterBagCommandHandler :
    IRequestHandler<CreateLetterBagCommand, BaseResponse<BagViewModel>>
{
    private readonly IBagRepository _bagRepository;
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IMapper _mapper;
    private readonly ShipmentSettings _shipmentSettings;

    public CreateLetterBagCommandHandler(
        IMapper mapper,
        IBagRepository bagRepository,
        IShipmentRepository shipmentRepository,
        ShipmentSettings shipmentSettings)
    {
        _mapper = mapper;
        _bagRepository = bagRepository;
        _shipmentRepository = shipmentRepository;
        _shipmentSettings = shipmentSettings;
    }

    public async Task<BaseResponse<BagViewModel>> Handle(
        CreateLetterBagCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateLetterBagCommandValidator(
            _bagRepository,
            _shipmentRepository,
            _shipmentSettings);
        var validationResult = await validator
            .ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            const string message = "Validation failed.";
            var invalidResponse =
                new BaseResponse<BagViewModel>(false, message);
            foreach (var error in validationResult.Errors)
            {
                invalidResponse
                    .ValidationErrors
                    .Add(error.ErrorMessage);
            }

            return invalidResponse;
        }

        var bag = _mapper.Map<Bag>(request);

        bag = await _bagRepository.AddAsync(bag);
        var bagModel = _mapper.Map<BagViewModel>(bag);

        var response = new BaseResponse<BagViewModel>(bagModel);
        return response;
    }
}
