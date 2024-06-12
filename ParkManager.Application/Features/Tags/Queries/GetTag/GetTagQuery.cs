using MediatR;

namespace ParkManager.Application.Features.Tags.Queries.GetTag
{
    public class GetTagQuery : IRequest<GetTagQueryResponse>
    {
        public Guid Id { get; set; }

        public GetTagQuery(Guid id)
        {
            Id = id;
        }
    }
}
