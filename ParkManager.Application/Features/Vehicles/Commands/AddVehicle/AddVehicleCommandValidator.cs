using FluentValidation;

namespace ParkManager.Application.Features.Vehicles.Commands.AddVehicle
{
    public class AddVehicleCommandValidator : AbstractValidator<AddVehicleCommand>
    {
        public AddVehicleCommandValidator()
        {
            RuleFor(p => p.Make).NotEmpty();
            RuleFor(p => p.Model).NotEmpty();
            RuleFor(p => p.Registration).NotEmpty();
        }
    }
}
