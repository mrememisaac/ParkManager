using MediatR;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Queries.GetDeparture
{
    public class GetDepartureQuery : IRequest<GetDepartureQueryResponse>
    {
        public Guid Id { get; set; }

        public GetDepartureQuery(Guid id)
        {
            Id = id;
        }
    }
}
