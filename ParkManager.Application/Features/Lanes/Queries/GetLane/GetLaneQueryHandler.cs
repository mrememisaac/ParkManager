using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Lanes.Queries.GetLane
{
    public class GetLaneQueryHandler : IRequestHandler<GetLaneQuery, GetLaneQueryResponse>
    {
        private readonly ILanesRepository _lanesRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetLaneQueryHandler> _logger;

        public GetLaneQueryHandler(ILanesRepository laneRepository, IMapper mapper, ILogger<GetLaneQueryHandler> logger)
        {
            _lanesRepository = laneRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetLaneQueryResponse> Handle(GetLaneQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving lane with id: {request.Id}");
            var lane = await _lanesRepository.Get(request.Id);
            return _mapper.Map< GetLaneQueryResponse>(lane);
        }
    }
}