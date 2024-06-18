using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Occasions.Queries.GetOccasion;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Queries.GetOccasions
{
    public class GetOccasionsQueryHandler : IRequestHandler<GetOccasionsQuery, GetOccasionsQueryResponse>
    {
        private readonly IOccasionsRepository _occasionsRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetOccasionsQueryHandler> _logger;

        public GetOccasionsQueryHandler(IOccasionsRepository occasionRepository, IMapper mapper, ILogger<GetOccasionsQueryHandler> logger)
        {
            _occasionsRepository = occasionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetOccasionsQueryResponse> Handle(GetOccasionsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetOccasionsQueryHandler - Retrieving - {request}");
            var occasions = await _occasionsRepository.List(request.Count, request.Page);
            return new GetOccasionsQueryResponse { Items = _mapper.Map<List<GetOccasionQueryResponse>>(occasions) };
        }
    }
}
