namespace ParkManager.Domain
{
    /// <summary>
    /// Represents a tag entity.
    /// </summary>
    public class Tag : Entity
    {
        /// <summary>
        /// Gets or sets the tag number.
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        /// <param name="number">The tag number.</param>
        public Tag(int number)
        {
            Number = number;
        }
    }
}
