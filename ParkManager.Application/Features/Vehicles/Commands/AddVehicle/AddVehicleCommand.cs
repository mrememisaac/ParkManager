using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Commands.AddVehicle
{
    public class AddVehicleCommand : IRequest<AddVehicleCommandResponse>
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
    }
}
