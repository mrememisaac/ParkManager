using System.Collections.ObjectModel;

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

        /// <summary>
        /// Gets a read-only collection of driver metadata.
        /// </summary>
        public ReadOnlyCollection<DriverMetadata> DriverDetails => _details.AsReadOnly();

        /// <summary>
        /// A list of driver metadata.
        /// This field is used internally to manage the collection of driver metadata.
        /// </summary>
        public List<DriverMetadata> _details = new List<DriverMetadata>();

        /// <summary>
        /// Adds a driver metadata to the collection.
        /// </summary>
        /// <param name="metadata">The driver metadata to add.</param>
        public void AddDriverMetadata(DriverMetadata metadata)
        {
            if (metadata is null) throw new ArgumentNullException($"{nameof(metadata)}");
            if (_details.Contains(metadata)) throw new ArgumentException("Metadata already exists in the collection.", $"{nameof(metadata)}");
            _details.Add(metadata);
        }

        /// <summary>
        /// Removes a driver metadata from the collection.
        /// </summary>
        /// <param name="metadata">The driver metadata to remove.</param>
        public void RemoveDriverMetadata(DriverMetadata metadata)
        {
            _details.Remove(metadata);
        }
    }
}
