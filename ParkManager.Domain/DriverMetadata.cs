using ParkManager.Domain.Exceptions;

namespace ParkManager.Domain
{
    /// <summary>
    /// Represents the metadata associated with a driver.
    /// </summary>
    public class DriverMetadata : Entity
    {
        /// <summary>
        /// Gets or sets the driver ID.
        /// </summary>
        public Guid DriverId { get; private set; }

        /// <summary>
        /// Gets or sets the key of the metadata.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets or sets the value of the metadata.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DriverMetadata"/> class.
        /// </summary>
        public DriverMetadata(Guid driverId, string key, string value)
        {
            if (driverId == Guid.Empty)
            {
                throw new EmptyGuidException("Driver ID cannot be empty.", nameof(driverId));
            }

            DriverId = driverId;
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
