
using ParkManager.Domain.Exceptions;

namespace ParkManager.Domain
{
    /// <summary>
    /// Represents an arrival image entity.
    /// </summary>
    public class ArrivalImage : Entity
    {
        public ArrivalImage(Guid arrivalId, string path)
        {
            if (arrivalId == Guid.Empty)
            {
                throw new EmptyGuidException("Arrival ID cannot be empty.", nameof(arrivalId));
            }

            ArrivalId = arrivalId;
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }

        /// <summary>
        /// Gets or sets the ID of the arrival associated with the image.
        /// </summary>
        public Guid ArrivalId { get; set; }

        /// <summary>
        /// Gets or sets the path of the image.
        /// </summary>
        public required string Path { get; set; }
    }
}
