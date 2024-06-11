using ParkManager.Domain.Exceptions;

namespace ParkManager.Domain
{
    /// <summary>
    /// Represents a lane in a park.
    /// </summary>
    public class Lane : Entity
    {
        /// <summary>
        /// Gets the ID of the park associated with the lane.
        /// </summary>
        public Guid ParkId { get; private set; }

        /// <summary>
        /// Gets or sets the name of the lane.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the park associated with the lane.
        /// </summary>
        public virtual Park Park { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Lane class.
        /// </summary>
        public Lane(Guid parkId, string name)
        {
            if (parkId == Guid.Empty)
            {
                throw new EmptyGuidException("Park ID cannot be empty.", nameof(parkId));
            }

            ParkId = parkId;
            Name = name;
        }
    }
}
