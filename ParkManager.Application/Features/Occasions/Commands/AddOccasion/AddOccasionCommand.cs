using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Commands.AddOccasion
{
    public class AddOccasionCommand : IRequest<AddOccasionCommandResponse>
    {
        public  Guid Id { get; set; }

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
