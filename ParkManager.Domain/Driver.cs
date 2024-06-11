namespace ParkManager.Domain
{
    /// <summary>
    /// Represents a driver entity.
    /// </summary>
    public class Driver : Entity
    {
        /// <summary>
        /// Gets or sets the name of the driver.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the phone number of the driver.
        /// </summary>
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Driver"/> class.
        /// </summary>
        /// <param name="name">The name of the driver.</param>
        /// <param name="phoneNumber">The phone number of the driver.</param>
        public Driver(string name, string phoneNumber)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        }
    }
}
