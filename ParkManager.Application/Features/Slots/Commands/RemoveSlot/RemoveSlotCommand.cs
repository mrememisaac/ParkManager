using MediatR;

namespace ParkManager.Application.Features.Slots.Commands.RemoveSlot
{
    public class RemoveSlotCommand : IRequest
    {
        public Guid Id { get; set; }

        public RemoveSlotCommand(Guid SlotId)
        {
            Id = SlotId;
        }
    }
}
