using System.Collections.ObjectModel;

namespace ParkManager.Domain
{
    /// <summary>
    /// Represents a departure from the park.
    /// </summary>
    public class Departure : ArrivalDepartureBase
    {
        /// <summary>
        /// Gets the collection of departure images.
        /// </summary>
        public ReadOnlyCollection<DepartureImage> Images => _images.AsReadOnly();

        /// <summary>
        /// A list of images associated with the departure event.
        /// This field is used internally to manage the collection of images.
        /// </summary>
        private List<DepartureImage> _images = new List<DepartureImage>();

        /// <summary>
        /// Initializes a new instance of the Departure class.
        /// </summary>
        /// <param name="departureId">The ID of the departure.</param>
        /// <param name="timestamp">The timestamp of the departure.</param>
        /// <param name="parkId">The ID of the park.</param>
        /// <param name="vehicleId">The ID of the vehicle.</param>
        /// <param name="driverId">The ID of the driver.</param>
        /// <param name="tagId">The ID of the tag.</param>
        public Departure(Guid departureId, DateTime timestamp, Guid parkId, Guid vehicleId, Guid driverId, Guid tagId) : base(timestamp, parkId, vehicleId, driverId, tagId)
        {
            Id = departureId;
        }


        /// <summary>
        /// Adds an image to the collection of images associated with the departure event.
        /// </summary>
        /// <param name="image">The image to add.</param>
        public void AddImage(DepartureImage image)
        {
            _images.Add(image);
        }

        /// <summary>
        /// Removes an image from the collection of images associated with the departure event.
        /// </summary>
        /// <param name="image">The image to remove.</param>
        public void RemoveImage(DepartureImage image)
        {
            _images.Remove(image);
        }
    }
}
