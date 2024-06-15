using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Tags.Queries.GetTags
{
    public record GetTagsQuery(int Page, int Count) : IRequest<GetTagsQueryResponse>
    { 
    }
}
