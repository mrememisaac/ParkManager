using MediatR;
using ParkManager.Application.Features.Departures.Commands.AddDeparture;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Commands.RemoveDeparture
{
    public class RemoveDepartureCommand : IRequest
    {
        public Guid DepartureId { get; set; }

        public RemoveDepartureCommand(Guid departureId)
        {
            DepartureId = departureId;
        }
    }
}
