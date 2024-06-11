using MediatR;

namespace ParkManager.Application.Features.Lanes.Commands.RemoveLane
{
    public class RemoveLaneCommand : IRequest
    {
        public Guid Id { get; set; }

        public RemoveLaneCommand(Guid LaneId)
        {
            Id = LaneId;
        }
    }
}
