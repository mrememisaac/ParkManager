using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Commands.UpdateDeparture
{
    public class UpdateDepartureCommand : IRequest<UpdateDepartureCommandResponse>
    {
        public DateTime Timestamp { get; set; }
        public int ParkId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int TagId { get; set; }
    }
}
