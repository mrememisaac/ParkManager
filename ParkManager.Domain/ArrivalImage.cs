﻿
using ParkManager.Domain.Exceptions;

namespace ParkManager.Domain
{
    /// <summary>
    /// Represents an arrival image entity.
    /// </summary>
    public class ArrivalImage : Entity
    {
        private ArrivalImage()
        {
            
        }
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
        public Guid ArrivalId { get; private set; }

        /// <summary>
        /// Gets or sets the path of the image.
        /// </summary>
        public string Path { get; private set; }
    }
}
