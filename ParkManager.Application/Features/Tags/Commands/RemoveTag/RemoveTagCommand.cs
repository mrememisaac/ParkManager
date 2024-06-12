using MediatR;
using ParkManager.Application.Features.Tags.Commands.AddTag;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Tags.Commands.RemoveTag
{
    public class RemoveTagCommand : IRequest
    {
        public Guid TagId { get; set; }

        public RemoveTagCommand(Guid tagId)
        {
            TagId = tagId;
        }
    }
}
