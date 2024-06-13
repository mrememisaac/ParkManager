using FluentValidation;

namespace ParkManager.Application.Features.Slots.Commands.AddSlot
{
    public class AddSlotCommandValidator : AbstractValidator<AddSlotCommand>
    {
        public AddSlotCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.LaneId).NotEmpty();
        }
    }
}
