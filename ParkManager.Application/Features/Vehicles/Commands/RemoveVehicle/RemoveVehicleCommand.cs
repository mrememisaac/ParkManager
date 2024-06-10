using MediatR;
using ParkManager.Application.Features.Vehicles.Commands.AddVehicle;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Commands.RemoveVehicle
{
    public class RemoveVehicleCommand : IRequest
    {
        public RemoveVehicleCommand(int vehicleId)
        {
            VehicleId = vehicleId;
        }

        public int VehicleId { get; set; }
    }
}
