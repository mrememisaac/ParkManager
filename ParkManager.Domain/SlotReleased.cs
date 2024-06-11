
namespace ParkManager.Domain
{
    /// <summary>
    /// Represents an event when a slot is released.
    /// </summary>
    public class SlotReleased : SlotEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlotReleased"/> class.
        /// </summary>
        /// <param name="slotId">The ID of the released slot.</param>
        /// <param name="vehicleId">The ID of the vehicle that was parked in the slot.</param>
        /// <param name="timestamp">The timestamp of the event.</param>
        public SlotReleased(Guid slotId, Guid vehicleId, DateTime timestamp) : base(slotId, vehicleId, timestamp)
        {
        }

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        public override EventType EventType { get; set; } = EventType.Released;
    }
}
