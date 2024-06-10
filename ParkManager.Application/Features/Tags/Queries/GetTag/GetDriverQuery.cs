using MediatR;

namespace ParkManager.Application.Features.Tags.Queries.GetTag
{
    public class GetTagQuery : IRequest<GetTagQueryResponse>
    {
        public int Id { get; set; }

        public GetTagQuery(int id)
        {
            Id = id;
        }
    }
}
