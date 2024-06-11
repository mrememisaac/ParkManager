using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Lanes.Queries.GetLane
{
    public class GetLaneQueryHandler : IRequestHandler<GetLaneQuery, Lane>
    {
        private readonly ILanesRepository _lanesRepository;
        
        public GetLaneQueryHandler(ILanesRepository laneRepository)
        {
            _lanesRepository = laneRepository;
        }

        public async Task<Lane> Handle(GetLaneQuery request, CancellationToken cancellationToken)
        {
            return await _lanesRepository.Get(request.Id);
        }
    }
}