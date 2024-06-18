using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Queries.GetDeparture
{
    public class GetDepartureQueryHandler : IRequestHandler<GetDepartureQuery, GetDepartureQueryResponse>
    {
        private readonly IDeparturesRepository _departuresRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetDepartureQueryHandler> _logger;

        public GetDepartureQueryHandler(IDeparturesRepository repository, IMapper mapper, ILogger<GetDepartureQueryHandler> logger)
        {
            _departuresRepository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetDepartureQueryResponse> Handle(GetDepartureQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving departure with id: {request.Id}");
            var departure = await _departuresRepository.Get(request.Id);
            return _mapper.Map< GetDepartureQueryResponse>(departure);
        }
    }
}
