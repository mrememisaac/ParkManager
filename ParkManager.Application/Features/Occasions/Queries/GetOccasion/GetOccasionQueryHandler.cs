using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Queries.GetOccasion
{
    public class GetOccasionQueryHandler : IRequestHandler<GetOccasionQuery, GetOccasionQueryResponse>
    {
        private readonly IOccasionsRepository _occasionsRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetOccasionQueryHandler> _logger;

        public GetOccasionQueryHandler(IOccasionsRepository occasionRepository, IMapper mapper, ILogger<GetOccasionQueryHandler> logger)
        {
            _occasionsRepository = occasionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetOccasionQueryResponse> Handle(GetOccasionQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetOccasionQueryHandler - Retrieving - {request}");
            var occassion = await _occasionsRepository.Get(request.Id);
            return _mapper.Map< GetOccasionQueryResponse>(occassion);
        }
    }
}