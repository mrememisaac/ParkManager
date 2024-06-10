using MediatR;
using ParkManager.Application.Features.Departures.Commands.AddDeparture;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Commands.RemoveDeparture
{
    public class RemoveDepartureCommand : IRequest
    {
        public int DepartureId { get; set; }

        public RemoveDepartureCommand(int departureId)
        {
            DepartureId = departureId;
        }
    }
}
