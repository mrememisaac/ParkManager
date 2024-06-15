using ParkManager.Application.Features.Parks.Queries.GetPark;

namespace ParkManager.Application.Features.Parks.Queries.GetParks
{
    public class GetParksQueryResponse
    {
        public List<GetParkQueryResponse> Results { get; set; } = new List<GetParkQueryResponse>();
    }
}
