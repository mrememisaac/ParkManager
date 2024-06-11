namespace ParkManager.Application.Features.Slots.Commands.UpdateSlot
{
    public class UpdateSlotCommandResponse
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the ID of the park associated with the Slot.
        /// </summary>
        public Guid SlotId { get; private set; }

        /// <summary>
        /// Gets or sets the name of the Slot.
        /// </summary>
        public string Name { get; private set; }
    }
}
