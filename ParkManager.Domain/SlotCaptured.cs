
namespace ParkManager.Domain
{
    /// <summary>
    /// Represents an event when a slot is captured.
    /// </summary>
    public class SlotCaptured : SlotEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlotCaptured"/> class.
        /// </summary>
        /// <param name="slotId">The ID of the captured slot.</param>
        /// <param name="vehicleId">The ID of the vehicle that captured the slot.</param>
        /// <param name="timestamp">The timestamp of the capture event.</param>
        public SlotCaptured(Guid slotId, Guid vehicleId, DateTime timestamp) : base(slotId, vehicleId, timestamp)
        {
        }

        /// <summary>
        /// Gets or sets the event type.
        /// </summary>
        public override EventType EventType { get; set; } = EventType.Captured;

    }
}
