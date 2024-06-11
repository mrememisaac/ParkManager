namespace ParkManager.Domain
{
    /// <summary>
    /// Represents a vehicle image.
    /// </summary>
    public class VehicleImage : Entity
    {
        /// <summary>
        /// Gets or sets the park ID associated with the vehicle image.
        /// </summary>
        public Guid ParkId { get; private set; }

        /// <summary>
        /// Gets or sets the path of the vehicle image.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleImage"/> class.
        /// </summary>
        /// <param name="parkId">The park ID associated with the vehicle image.</param>
        /// <param name="path">The path of the vehicle image.</param>
        public VehicleImage(Guid parkId, string path)
        {
            Path = !string.IsNullOrEmpty(path) ? path : throw new ArgumentNullException(nameof(path));
            if (parkId == Guid.Empty)
            {
                throw new ArgumentException("Park ID cannot be empty.", nameof(parkId));
            }
            ParkId = parkId;
        }
    }
}
