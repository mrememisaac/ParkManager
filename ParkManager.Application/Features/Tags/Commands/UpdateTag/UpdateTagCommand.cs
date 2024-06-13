using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Tags.Commands.UpdateTag
{
    public class UpdateTagCommand : IRequest<UpdateTagCommandResponse>
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
    }
}
