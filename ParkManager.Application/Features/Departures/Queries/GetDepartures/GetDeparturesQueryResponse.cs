using ParkManager.Application.Features.Departures.Queries.GetDeparture;

namespace ParkManager.Application.Features.Departures.Queries.GetDepartures
{
    public class GetDeparturesQueryResponse
    {
        public List<GetDepartureQueryResponse> Items { get; set; } = new List<GetDepartureQueryResponse>();
    }
}
