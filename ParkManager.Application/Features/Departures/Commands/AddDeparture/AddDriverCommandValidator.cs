using FluentValidation;

namespace ParkManager.Application.Features.Departures.Commands.AddDeparture
{
    public class AddDepartureCommandValidator : AbstractValidator<AddDepartureCommand>
    {
        public AddDepartureCommandValidator()
        {
            RuleFor(p => p.Timestamp).NotEmpty();
            RuleFor(p => p.VehicleId).NotEmpty();
            RuleFor(p => p.DriverId).NotEmpty();
            RuleFor(p => p.ParkId).NotEmpty();
            RuleFor(p => p.TagId).NotEmpty();
        }
    }
}
