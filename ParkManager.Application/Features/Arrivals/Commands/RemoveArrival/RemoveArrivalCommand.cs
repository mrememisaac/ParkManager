using MediatR;
using ParkManager.Application.Features.Arrivals.Commands.AddArrival;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Commands.RemoveArrival
{
    public class RemoveArrivalCommand : IRequest
    {
        public Guid ArrivalId { get; set; }

        public RemoveArrivalCommand(Guid arrivalId)
        {
            ArrivalId = arrivalId;
        }
    }
}
