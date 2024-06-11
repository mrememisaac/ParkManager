using MediatR;
using ParkManager.Application.Features.Vehicles.Commands.AddVehicle;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Commands.RemoveVehicle
{
    public class RemoveVehicleCommand : IRequest
    {
        public RemoveVehicleCommand(Guid vehicleId)
        {
            VehicleId = vehicleId;
        }

        public Guid VehicleId { get; set; }
    }
}
