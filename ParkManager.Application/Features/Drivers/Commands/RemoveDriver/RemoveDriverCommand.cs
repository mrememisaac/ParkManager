using MediatR;
using ParkManager.Application.Features.Drivers.Commands.AddDriver;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Drivers.Commands.RemoveDriver
{
    public class RemoveDriverCommand : IRequest
    {
        public RemoveDriverCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
