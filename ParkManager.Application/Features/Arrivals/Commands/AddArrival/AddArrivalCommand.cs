using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Commands.AddArrival
{
    public class AddArrivalCommand : IRequest<AddArrivalCommandResponse>
    {
        public DateTime Timestamp { get; set; }
        public int ParkId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int TagId { get; set; }
    }
}
