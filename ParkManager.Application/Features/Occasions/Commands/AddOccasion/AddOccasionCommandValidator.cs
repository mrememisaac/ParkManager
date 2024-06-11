using FluentValidation;

namespace ParkManager.Application.Features.Occasions.Commands.AddOccasion
{
    public class AddOccasionCommandValidator : AbstractValidator<AddOccasionCommand>
    {
        public AddOccasionCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.EndDate).NotEmpty();
            RuleFor(p => p.StartDate).NotEmpty();
        }
    }
}
