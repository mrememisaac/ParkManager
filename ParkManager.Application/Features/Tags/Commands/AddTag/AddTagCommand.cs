using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Tags.Commands.AddTag
{
    public class AddTagCommand : IRequest<AddTagCommandResponse>
    {
        public int Number { get; set; }
    }
}
