using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Lanes.Queries.GetLane
{
    public class GetLaneQueryHandler : IRequestHandler<GetLaneQuery, GetLaneQueryResponse>
    {
        private readonly ILanesRepository _lanesRepository;
        private readonly IMapper _mapper;

        public GetLaneQueryHandler(ILanesRepository laneRepository, IMapper mapper)
        {
            _lanesRepository = laneRepository;
            _mapper = mapper;
        }

        public async Task<GetLaneQueryResponse> Handle(GetLaneQuery request, CancellationToken cancellationToken)
        {
            var lane = await _lanesRepository.Get(request.Id);
            return _mapper.Map< GetLaneQueryResponse>(lane);
        }
    }
}