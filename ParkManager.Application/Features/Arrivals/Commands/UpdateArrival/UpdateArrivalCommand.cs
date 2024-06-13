using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Commands.UpdateArrival
{
    public class UpdateArrivalCommand : IRequest<UpdateArrivalCommandResponse>
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid ParkId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid DriverId { get; set; }
        public Guid TagId { get; set; }
    }
}
