using ParkManager.Application.Features.Arrivals.Queries.GetArrival;

namespace ParkManager.Application.Features.Arrivals.Queries.GetArrivals
{
    public class GetArrivalsQueryResponse
    {
        public List<GetArrivalQueryResponse> Results { get; set; } = new List<GetArrivalQueryResponse>();
    }
}
