using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Queries.GetPark
{
    public class GetParkQueryHandler : IRequestHandler<GetParkQuery, GetParkQueryResponse>
    {
        private readonly IParksRepository _parksRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetParkQueryHandler> _logger;

        public GetParkQueryHandler(IParksRepository parkRepository, IMapper mapper, ILogger<GetParkQueryHandler> logger)
        {
            _parksRepository = parkRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetParkQueryResponse> Handle(GetParkQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetParkQueryHandler - Retrieving Park - {request.Id}");
            var park = await _parksRepository.Get(request.Id);
            return _mapper.Map< GetParkQueryResponse>(park);
        }
    }
}