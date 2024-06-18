using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Queries.GetArrival
{
    public class GetArrivalQueryHandler : IRequestHandler<GetArrivalQuery, GetArrivalQueryResponse>
    {
        private readonly IArrivalsRepository _arrivalsRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetArrivalQueryHandler> _logger;
        private readonly IDistributedCache _cache;

        public GetArrivalQueryHandler(IArrivalsRepository repository, IMapper mapper, ILogger<GetArrivalQueryHandler> logger)
        {
            _arrivalsRepository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetArrivalQueryResponse> Handle(GetArrivalQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving Arrival with Id: {request.Id}");
            var response = await _arrivalsRepository.Get(request.Id);
            return _mapper.Map<GetArrivalQueryResponse>(response);
        }
    }
}
