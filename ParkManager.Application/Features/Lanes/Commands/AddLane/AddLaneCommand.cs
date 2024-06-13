using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Lanes.Commands.AddLane
{
    public class AddLaneCommand : IRequest<AddLaneCommandResponse>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the ID of the park associated with the lane.
        /// </summary>
        public Guid ParkId { get; set; }

        /// <summary>
        /// Gets or sets the name of the lane.
        /// </summary>
        public string Name { get; set; }

    }
}
