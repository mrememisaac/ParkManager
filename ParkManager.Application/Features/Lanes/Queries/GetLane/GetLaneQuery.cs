using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Lanes.Queries.GetLane
{
    public class GetLaneQuery : IRequest<Lane>
    {
        public Guid Id { get; set; }

        public GetLaneQuery(Guid id)
        {
            Id = id;
        }
    }
}
