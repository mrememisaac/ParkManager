using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Tags.Commands.AddTag
{
    public class AddTagCommand : IRequest<AddTagCommandResponse>
    {
        public Guid Id { get; set; }

        public int Number { get; set; }
    }
}
