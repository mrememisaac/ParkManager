namespace ParkManager.Domain
{
    /// <summary>
    /// Represents a parking slot.
    /// </summary>
    public class Slot : Entity
    {
        /// <summary>
        /// Gets the ID of the lane that the slot belongs to.
        /// </summary>
        public Guid LaneId { get; private set; }

        /// <summary>
        /// Gets or sets the name of the slot.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the lane that the slot belongs to.
        /// </summary>
        public virtual Lane Lane { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slot"/> class.
        /// </summary>
        public Slot(Guid laneId, string name)
        {
            if (laneId == Guid.Empty)
            {
                throw new ArgumentException("Lane ID cannot be empty.", nameof(laneId));
            }

            LaneId = laneId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
