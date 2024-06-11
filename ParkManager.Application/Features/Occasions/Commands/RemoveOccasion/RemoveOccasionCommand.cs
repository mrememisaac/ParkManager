using MediatR;

namespace ParkManager.Application.Features.Occasions.Commands.RemoveOccasion
{
    public class RemoveOccasionCommand : IRequest
    {
        public Guid Id { get; set; }

        public RemoveOccasionCommand(Guid OccasionId)
        {
            Id = OccasionId;
        }
    }
}
