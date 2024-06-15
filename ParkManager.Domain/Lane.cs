using ParkManager.Domain.Exceptions;
using System.Collections.ObjectModel;

namespace ParkManager.Domain
{
    /// <summary>
    /// Represents a lane in a park.
    /// </summary>
    public class Lane : Entity
    {
        private Lane()
        {
            
        }
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
        /// Gets a read-only collection of Lane slots.
        /// </summary>
        public IReadOnlyCollection<Slot> Slots => _slots.AsReadOnly();

        private List<Slot> _slots = new List<Slot>();

        /// <summary>
        /// Initializes a new instance of the Lane class.
        /// </summary>
        /// <param name="parkId">The ID of the park associated with the lane.</param>
        /// <param name="name">The name of the lane.</param>
        /// <exception cref="EmptyGuidException">Thrown when the park ID is empty.</exception>
        public Lane(Guid laneId, Guid parkId, string name)
        {
            if (parkId == Guid.Empty)
            {
                throw new EmptyGuidException("Park ID cannot be empty.", nameof(parkId));
            }
            if (laneId == Guid.Empty)
            {
                throw new EmptyGuidException("Lane ID cannot be empty.", nameof(laneId));
            }
            Id = laneId;
            ParkId = parkId;
            Name = name;
        }

        /// <summary>
        /// Adds a slot to the lane.
        /// </summary>
        /// <param name="slot">The slot to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when the slot is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the slot has already been added.</exception>
        public void AddSlot(Slot slot)
        {
            if (slot == null)
            {
                throw new ArgumentNullException(nameof(slot));
            }

            if (_slots.Contains(slot))
            {
                throw new ArgumentException("The slot has already been added.", nameof(slot));
            }

            _slots.Add(slot);
        }

        /// <summary>
        /// Removes a slot from the lane.
        /// </summary>
        /// <param name="slot">The slot to remove.</param>
        /// <exception cref="ArgumentNullException">Thrown when the slot is null.</exception>
        public void RemoveSlot(Slot slot)
        {
            if (slot == null)
            {
                throw new ArgumentNullException(nameof(slot));
            }

            _slots.Remove(slot);
        }
    }
}
