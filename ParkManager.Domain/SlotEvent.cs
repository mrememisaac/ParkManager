using ParkManager.Domain.Exceptions;

namespace ParkManager.Domain
{
    /// <summary>
    /// Represents an abstract base class for slot events.
    /// </summary>
    public abstract class SlotEvent : Entity
    {
        /// <summary>
        /// Gets or sets the ID of the slot associated with the event.
        /// </summary>
        public Guid SlotId { get; protected set; }

        /// <summary>
        /// Gets or sets the ID of the vehicle associated with the event.
        /// </summary>
        public Guid VehicleId { get; protected set; }

        /// <summary>
        /// Gets or sets the timestamp of the event.
        /// </summary>
        public DateTime Timestamp { get; protected set; }

        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        public virtual EventType EventType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlotEvent"/> class.
        /// </summary>
        /// <param name="slotId">The ID of the slot associated with the event.</param>
        /// <param name="vehicleId">The ID of the vehicle associated with the event.</param>
        /// <param name="timestamp">The timestamp of the event.</param>
        /// <exception cref="EmptyGuidException">Thrown when either <paramref name="slotId"/> or <paramref name="vehicleId"/> is an empty Guid.</exception>
        public SlotEvent(Guid slotId, Guid vehicleId, DateTime timestamp)
        {
            SlotId = Guid.Empty == slotId ? throw new EmptyGuidException($"{nameof(slotId)} cannot be an empty Guid") : slotId;
            VehicleId = Guid.Empty == vehicleId ? throw new EmptyGuidException($"{nameof(vehicleId)} cannot be an empty Guid") : vehicleId;
            Timestamp = timestamp;
        }
    }
}
