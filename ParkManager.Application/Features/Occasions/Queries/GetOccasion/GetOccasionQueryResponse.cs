namespace ParkManager.Application.Features.Occasions.Queries.GetOccasion
{
    public class GetOccasionQueryResponse
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the occasion.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the start date of the occasion.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the occasion.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
