using System.Collections.ObjectModel;

namespace ParkManager.Domain
{
    /// <summary>
    /// Represents an arrival event in the park management domain.
    /// This class inherits from the ArrivalDepartureBase class.
    /// </summary>
    public class Arrival : ArrivalDepartureBase
    {
        private Arrival() : base(DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid())
        {
            
        }

        /// <summary>
        /// Gets a read-only collection of images associated with the arrival event.
        /// </summary>
        public IReadOnlyCollection<ArrivalImage> Images => (_images as List<ArrivalImage>).AsReadOnly();

        /// <summary>
        /// A list of images associated with the arrival event.
        /// This field is used internally to manage the collection of images.
        /// </summary>
        public List<ArrivalImage> _images = new List<ArrivalImage>();

        /// <summary>
        /// Initializes a new instance of the Arrival class with the specified parameters.
        /// </summary>
        /// <param name="arrivalId">The ID of the arrival event.</param>
        /// <param name="timestamp">The timestamp of the arrival event.</param>
        /// <param name="parkId">The ID of the park associated with the arrival event.</param>
        /// <param name="vehicleId">The ID of the vehicle associated with the arrival event.</param>
        /// <param name="driverId">The ID of the driver associated with the arrival event.</param>
        /// <param name="tagId">The ID of the tag associated with the arrival event.</param>
        public Arrival(Guid arrivalId, DateTime timestamp, Guid parkId, Guid vehicleId, Guid driverId, Guid tagId) : base(timestamp, parkId, vehicleId, driverId, tagId)
        {
            Id = arrivalId;
        }

        /// <summary>
        /// Adds an image to the collection of images associated with the arrival event.
        /// </summary>
        /// <param name="image">The image to add.</param>
        public void AddImage(ArrivalImage image)
        {
            _images.Add(image);
        }

        /// <summary>
        /// Removes an image from the collection of images associated with the arrival event.
        /// </summary>
        /// <param name="image">The image to remove.</param>
        public void RemoveImage(ArrivalImage image)
        {
            _images.Remove(image);
        }
    }
}
