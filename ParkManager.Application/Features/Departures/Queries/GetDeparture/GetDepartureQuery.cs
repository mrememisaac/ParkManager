using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Queries.GetDeparture
{
    public class GetDepartureQuery : IRequest<Departure>
    {
        public int Id { get; set; }

        public GetDepartureQuery(int id)
        {
            Id = id;
        }
    }
}
