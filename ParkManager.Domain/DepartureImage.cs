using ParkManager.Domain.Exceptions;

namespace ParkManager.Domain
{
    /// <summary>
    /// Represents an image associated with a departure event in the park management domain.
    /// This class inherits from the Entity base class.
    /// </summary>
    public class DepartureImage : Entity
    {
        private DepartureImage()
        {
            
        }

        /// <summary>
        /// Gets or sets the identifier of the departure event associated with the image.
        /// </summary>
        public Guid DepartureId { get; private set; }

        /// <summary>
        /// Gets or sets the path of the image file.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Initializes a new instance of the DepartureImage class.
        /// </summary>
        public DepartureImage(Guid departureId, string path)
        {
            if (departureId == Guid.Empty)
            {
                throw new EmptyGuidException("DepartureId cannot be empty.", nameof(departureId));
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", nameof(path));
            }

            DepartureId = departureId;
            Path = path;
        }
    }
}
