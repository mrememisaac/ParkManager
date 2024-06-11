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
        public Vehicle(string make, string model, string registration)
        {
            Make = make ?? throw new ArgumentNullException(nameof(make));
            Model = model ?? throw new ArgumentNullException(nameof(model));
            Registration = registration ?? throw new ArgumentNullException(nameof(registration));
        }
    }
}
