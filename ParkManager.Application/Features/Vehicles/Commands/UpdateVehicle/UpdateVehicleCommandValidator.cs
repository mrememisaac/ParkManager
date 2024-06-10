using FluentValidation;

namespace ParkManager.Application.Features.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {
            RuleFor(p => p.Model).NotEmpty();
            RuleFor(p => p.Make).NotEmpty();
            RuleFor(p => p.Registration).NotEmpty();
        }
    }
}
