using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Commands.UpdateArrival
{
    public class UpdateArrivalCommand : IRequest<UpdateArrivalCommandResponse>
    {
        public DateTime Timestamp { get; set; }
        public int ParkId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int TagId { get; set; }
    }
}
