using FluentValidation;

namespace ParkManager.Application.Features.Arrivals.Commands.UpdateArrival
{
    public class UpdateArrivalCommandValidator : AbstractValidator<UpdateArrivalCommand>
    {
        public UpdateArrivalCommandValidator()
        {
            RuleFor(p => p.Timestamp).NotEmpty();
            RuleFor(p => p.VehicleId).NotEmpty();
            RuleFor(p => p.DriverId).NotEmpty();
            RuleFor(p => p.ParkId).NotEmpty();
            RuleFor(p => p.TagId).NotEmpty();
        }
    }
}
