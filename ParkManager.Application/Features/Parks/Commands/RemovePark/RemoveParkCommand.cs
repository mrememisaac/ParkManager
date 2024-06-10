using MediatR;

namespace ParkManager.Application.Features.Parks.Commands.RemovePark
{
    public class RemoveParkCommand : IRequest
    {
        public int Id { get; set; }

        public RemoveParkCommand(int parkId)
        {
            Id = parkId;
        }
    }
}
