namespace ParkManager.Application.Features.Lanes.Queries.GetLane
{
    public class GetLaneQueryResponse
    {
        public Guid Id { get; set; }
        public Guid ParkId { get; set; }
        
        public string Name { get; set; }
       
    }
}
