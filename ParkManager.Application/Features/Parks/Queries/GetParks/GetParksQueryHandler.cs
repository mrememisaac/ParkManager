using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Parks.Queries.GetPark;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Queries.GetParks
{
    public class GetParksQueryHandler : IRequestHandler<GetParksQuery, GetParksQueryResponse>
    {
        private readonly IParksRepository _parksRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetParksQueryHandler> _logger;

        public GetParksQueryHandler(IParksRepository parkRepository, IMapper mapper, ILogger<GetParksQueryHandler> logger)
        {
            _parksRepository = parkRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetParksQueryResponse> Handle(GetParksQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetParksQueryHandler - Retrieving Parks - {request}");
            var parks = await _parksRepository.List(request.Count, request.Page);
            return new GetParksQueryResponse{ Items = _mapper.Map<List<GetParkQueryResponse>>(parks)};
        }
    }
}
