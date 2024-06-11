namespace ParkManager.Domain
{
    /// <summary>
    /// Represents an image associated with a park.
    /// </summary>
    public class ParkImage : Entity
    {
        /// <summary>
        /// Gets or sets the ID of the park.
        /// </summary>
        public Guid ParkId { get; set; }

        /// <summary>
        /// Gets or sets the path of the image file.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the associated park.
        /// </summary>
        public virtual Park Park { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParkImage"/> class.
        /// </summary>
        /// <param name="parkId">The ID of the park.</param>
        /// <param name="path">The path of the image file.</param>
        public ParkImage(Guid parkId, string path)
        {
            if (parkId == Guid.Empty)
            {
                throw new ArgumentException("Park ID cannot be empty.", nameof(parkId));
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Image path cannot be null or empty.", nameof(path));
            }

            ParkId = parkId;
            Path = path;
        }
    }
}
