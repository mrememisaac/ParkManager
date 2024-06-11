using FluentValidation;

namespace ParkManager.Application.Features.Slots.Commands.UpdateSlot
{
    public class UpdateSlotCommandValidator : AbstractValidator<UpdateSlotCommand>
    {
        public UpdateSlotCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.SlotId).NotEmpty();
            
        }
    }
}
