using FluentValidation;

namespace ParkManager.Application.Features.Drivers.Commands.AddDriver
{
    public class AddDriverCommandValidator : AbstractValidator<AddDriverCommand>
    {
        public AddDriverCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.PhoneNumber).NotEmpty();
        }
    }
}
