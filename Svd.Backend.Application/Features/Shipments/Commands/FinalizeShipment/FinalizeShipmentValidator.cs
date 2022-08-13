using FluentValidation;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Features.Shipments.Commands.FinalizeShipment;

public class FinalizeShipmentValidator : AbstractValidator<Shipment>
{
    public FinalizeShipmentValidator()
    {
        RuleFor(shipment => shipment.FlightDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Flight date is in the past.");

        RuleFor(shipment => shipment.Bags)
            .NotEmpty();
    }
}
