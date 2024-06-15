using ParkManager.Application.Features.Departures.Queries.GetDeparture;

namespace ParkManager.Application.Features.Departures.Queries.GetDepartures
{
    public class GetDeparturesQueryResponse
    {
        public List<GetDepartureQueryResponse> Results { get; set; } = new List<GetDepartureQueryResponse>();
    }
}
