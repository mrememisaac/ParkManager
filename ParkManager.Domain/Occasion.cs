namespace ParkManager.Domain
{
    /// <summary>
    /// Represents an occasion.
    /// </summary>
    public class Occasion : Entity
    {
        /// <summary>
        /// Gets or sets the name of the occasion.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the start date of the occasion.
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Gets or sets the end date of the occasion.
        /// </summary>
        public DateTime EndDate { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Occasion"/> class.
        /// </summary>
        /// <param name="name">The name of the occasion.</param>
        /// <param name="startDate">The start date of the occasion.</param>
        /// <param name="endDate">The end date of the occasion.</param>
        public Occasion(Guid id, string name, DateTime startDate, DateTime endDate)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            if(startDate > endDate)
                throw new ArgumentException("Start date must be before end date.");
            if(startDate.DayOfYear < DateTime.Now.DayOfYear)
                throw new ArgumentException("Start cannot be in the past.");
            Id = id == Guid.Empty ? throw new ArgumentException("Id cannot be empty") : id;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
