using MediatR;

namespace ParkManager.Application.Features.Parks.Commands.RemovePark
{
    public class RemoveParkCommand : IRequest
    {
        public Guid Id { get; set; }

        public RemoveParkCommand(Guid parkId)
        {
            Id = parkId;
        }
    }
}
