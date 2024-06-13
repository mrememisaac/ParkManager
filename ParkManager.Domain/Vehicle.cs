using System.Collections.ObjectModel;

namespace ParkManager.Domain
{
    /// <summary>
    /// Represents a vehicle.
    /// </summary>
    public class Vehicle : Entity
    {
        /// <summary>
        /// Gets or sets the make of the vehicle.
        /// </summary>
        public string Make { get; private set; }

        /// <summary>
        /// Gets or sets the model of the vehicle.
        /// </summary>
        public string Model { get; private set; }

        /// <summary>
        /// Gets or sets the registration number of the vehicle.
        /// </summary>
        public string Registration { get; private set; }

        /// <summary>
        /// Gets the read-only collection of vehicle images.
        /// </summary>
        public ReadOnlyCollection<VehicleImage> Images => _images.AsReadOnly();

        private List<VehicleImage> _images = new List<VehicleImage>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="make">The make of the vehicle.</param>
        /// <param name="model">The model of the vehicle.</param>
        /// <param name="registration">The registration number of the vehicle.</param>
        public Vehicle(Guid id, string make, string model, string registration)
        {
            Id = id == Guid.Empty ? throw new ArgumentException("Id cannot be empty.", nameof(id)) : id;
            Make = make ?? throw new ArgumentNullException(nameof(make));
            Model = model ?? throw new ArgumentNullException(nameof(model));
            Registration = registration ?? throw new ArgumentNullException(nameof(registration));
        }

        /// <summary>
        /// Adds a vehicle image to the collection.
        /// </summary>
        /// <param name="image">The vehicle image to add.</param>
        public void AddImage(VehicleImage image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (_images.Contains(image))
            {
                throw new ArgumentException("Image already exists in the collection.", nameof(image));
            }

            _images.Add(image);
        }

        /// <summary>
        /// Removes a vehicle image from the collection.
        /// </summary>
        /// <param name="image">The vehicle image to remove.</param>
        /// <returns>True if the image was successfully removed; otherwise, false.</returns>
        public bool RemoveImage(VehicleImage image)
        {
            return _images.Remove(image);
        }
    }
}
