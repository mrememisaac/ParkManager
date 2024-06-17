using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Lanes.Queries.GetLane;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Lanes.Queries.GetLanes
{
    public class GetLanesQueryHandler : IRequestHandler<GetLanesQuery, GetLanesQueryResponse>
    {
        private readonly ILanesRepository _lanesRepository;
        private readonly IMapper _mapper;

        public GetLanesQueryHandler(ILanesRepository laneRepository, IMapper mapper)
        {
            _lanesRepository = laneRepository;
            _mapper = mapper;
        }

        public async Task<GetLanesQueryResponse> Handle(GetLanesQuery request, CancellationToken cancellationToken)
        {
            var lanes = await _lanesRepository.List(request.Count, request.Page);
            return new GetLanesQueryResponse { Items = _mapper.Map<List<GetLaneQueryResponse>>(lanes) };
        }
    }
}
