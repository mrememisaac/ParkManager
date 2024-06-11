using FluentValidation;

namespace ParkManager.Application.Features.Occasions.Commands.UpdateOccasion
{
    public class UpdateOccasionCommandValidator : AbstractValidator<UpdateOccasionCommand>
    {
        public UpdateOccasionCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.EndDate).NotEmpty();
            RuleFor(p => p.StartDate).NotEmpty();
        }
    }
}
