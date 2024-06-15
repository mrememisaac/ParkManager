using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Lanes.Queries.GetLane
{
    public class GetLaneQuery : IRequest<GetLaneQueryResponse>
    {
        public Guid Id { get; set; }

        public GetLaneQuery(Guid id)
        {
            Id = id;
        }
    }
}
