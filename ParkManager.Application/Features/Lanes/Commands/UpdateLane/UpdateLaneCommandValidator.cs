using FluentValidation;

namespace ParkManager.Application.Features.Lanes.Commands.UpdateLane
{
    public class UpdateLaneCommandValidator : AbstractValidator<UpdateLaneCommand>
    {
        public UpdateLaneCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.ParkId).NotEmpty();
            
        }
    }
}
