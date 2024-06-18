using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Lanes.Queries.GetLane;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Lanes.Queries.GetLanes
{
    public class GetLanesQueryHandler : IRequestHandler<GetLanesQuery, GetLanesQueryResponse>
    {
        private readonly ILanesRepository _lanesRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetLanesQueryHandler> _logger;

        public GetLanesQueryHandler(ILanesRepository laneRepository, IMapper mapper, ILogger<GetLanesQueryHandler> logger)
        {
            _lanesRepository = laneRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetLanesQueryResponse> Handle(GetLanesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetLanesQueryHandler - {request}");
            var lanes = await _lanesRepository.List(request.Count, request.Page);
            return new GetLanesQueryResponse { Items = _mapper.Map<List<GetLaneQueryResponse>>(lanes) };
        }
    }
}
