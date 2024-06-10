using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommand : IRequest<UpdateVehicleCommandResponse>
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
    }
}
