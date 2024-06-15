using ParkManager.Application.Features.Occasions.Queries.GetOccasion;

namespace ParkManager.Application.Features.Occasions.Queries.GetOccasions
{
    public class GetOccasionsQueryResponse
    {
        public List<GetOccasionQueryResponse> Results { get; set; } = new List<GetOccasionQueryResponse>();
    }
}
