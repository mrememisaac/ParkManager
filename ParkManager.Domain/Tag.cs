namespace ParkManager.Domain
{
    /// <summary>
    /// Represents a tag entity.
    /// </summary>
    public class Tag : Entity
    {
        private Tag()
        {
            
        }
        /// <summary>
        /// Gets or sets the tag number.
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        /// <param name="number">The tag number.</param>
        public Tag(Guid id, int number)
        {
            Id = id != Guid.Empty ? id : throw new ArgumentException("Id cannot be empty");
            if (number < 0)
                throw new ArgumentException("Tag number cannot be negative.", nameof(number));
            Number = number;
        }
    }
}
