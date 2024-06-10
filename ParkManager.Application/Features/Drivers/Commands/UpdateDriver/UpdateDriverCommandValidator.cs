using FluentValidation;

namespace ParkManager.Application.Features.Drivers.Commands.UpdateDriver
{
    public class UpdateDriverCommandValidator : AbstractValidator<UpdateDriverCommand>
    {
        public UpdateDriverCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.PhoneNumber).NotEmpty();
        }
    }
}
