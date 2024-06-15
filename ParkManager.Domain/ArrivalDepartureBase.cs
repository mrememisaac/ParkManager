using ParkManager.Domain.Exceptions;

namespace ParkManager.Domain
{
    /// <summary>
    /// Represents the base class for arrival and departure entities.
    /// </summary>
    public abstract class ArrivalDepartureBase : Entity
    {
        /// <summary>
        /// Gets or sets the timestamp of the arrival or departure.
        /// </summary>
        public DateTime Timestamp { get; protected set; } = default;

        /// <summary>
        /// Gets or sets the ID of the park associated with the arrival or departure.
        /// </summary>
        public Guid ParkId { get; protected set; } = default;

        /// <summary>
        /// Gets or sets the ID of the vehicle associated with the arrival or departure.
        /// </summary>
        public Guid VehicleId { get; protected set; } = default;

        /// <summary>
        /// Gets or sets the ID of the driver associated with the arrival or departure.
        /// </summary>
        public Guid DriverId { get; protected set; } = default;

        /// <summary>
        /// Gets or sets the ID of the tag associated with the arrival or departure.
        /// </summary>
        public Guid TagId { get; protected set; } = default;

        /// <summary>
        /// Initializes a new instance of the ArrivalDepartureBase class.
        /// </summary>
        public ArrivalDepartureBase(DateTime timestamp, Guid parkId, Guid vehicleId, Guid driverId, Guid tagId)
        {
            if (parkId == Guid.Empty)
                throw new EmptyGuidException("Park ID cannot be empty.", nameof(parkId));

            if (vehicleId == Guid.Empty)
                throw new EmptyGuidException("Vehicle ID cannot be empty.", nameof(vehicleId));

            if (driverId == Guid.Empty)
                throw new EmptyGuidException("Driver ID cannot be empty.", nameof(driverId));

            if (tagId == Guid.Empty)
                throw new EmptyGuidException("Tag ID cannot be empty.", nameof(tagId));

            Timestamp = DateTime.Now;
            ParkId = parkId;
            VehicleId = vehicleId;
            DriverId = driverId;
            TagId = tagId;
        }
    }
}
