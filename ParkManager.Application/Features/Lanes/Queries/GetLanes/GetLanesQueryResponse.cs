using ParkManager.Application.Features.Lanes.Queries.GetLane;

namespace ParkManager.Application.Features.Lanes.Queries.GetLanes
{
    public class GetLanesQueryResponse
    {
        public List<GetLaneQueryResponse> Items { get; set; } = new List<GetLaneQueryResponse>();
    }
}
