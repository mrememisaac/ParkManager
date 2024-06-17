using ParkManager.Application.Features.Tags.Queries.GetTag;

namespace ParkManager.Application.Features.Tags.Queries.GetTags
{
    public class GetTagsQueryResponse
    {
        public List<GetTagQueryResponse> Items { get; set; } = new List<GetTagQueryResponse>();
    }
}
