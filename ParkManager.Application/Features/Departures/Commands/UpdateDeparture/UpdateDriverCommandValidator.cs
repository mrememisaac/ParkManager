using FluentValidation;

namespace ParkManager.Application.Features.Departures.Commands.UpdateDeparture
{
    public class UpdateDepartureCommandValidator : AbstractValidator<UpdateDepartureCommand>
    {
        public UpdateDepartureCommandValidator()
        {
            RuleFor(p => p.Timestamp).NotEmpty();
            RuleFor(p => p.VehicleId).NotEmpty();
            RuleFor(p => p.DriverId).NotEmpty();
            RuleFor(p => p.ParkId).NotEmpty();
            RuleFor(p => p.TagId).NotEmpty();
        }
    }
}
