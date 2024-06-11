using FluentValidation;

namespace ParkManager.Application.Features.Lanes.Commands.AddLane
{
    public class AddLaneCommandValidator : AbstractValidator<AddLaneCommand>
    {
        public AddLaneCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.ParkId).NotEmpty();
        }
    }
}
