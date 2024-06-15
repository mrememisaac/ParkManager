namespace ParkManager.Domain
{
    /// <summary>
    /// Represents a vehicle image.
    /// </summary>
    public class VehicleImage : Entity
    {
        private VehicleImage()
        {
            
        }
        /// <summary>
        /// Gets or sets the park ID associated with the vehicle image.
        /// </summary>
        public Guid VehicleId { get; private set; }

        /// <summary>
        /// Gets or sets the path of the vehicle image.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleImage"/> class.
        /// </summary>
        /// <param name="vehicleId">The vehicle ID associated with the vehicle image.</param>
        /// <param name="path">The path of the vehicle image.</param>
        public VehicleImage(Guid vehicleId, string path)
        {
            Path = !string.IsNullOrEmpty(path) ? path : throw new ArgumentNullException(nameof(path));
            if (vehicleId == Guid.Empty)
            {
                throw new ArgumentException("Park ID cannot be empty.", nameof(vehicleId));
            }
            VehicleId = vehicleId;
        }
    }
}
