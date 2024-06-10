using FluentValidation;

namespace ParkManager.Application.Features.Tags.Commands.AddTag
{
    public class AddTagCommandValidator : AbstractValidator<AddTagCommand>
    {
        public AddTagCommandValidator()
        {
            RuleFor(p => p.Number).NotEmpty();
        }
    }
}
