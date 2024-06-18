using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Departures.Queries.GetDeparture;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Queries.GetDepartures
{
    public class GetDeparturesQueryHandler : IRequestHandler<GetDeparturesQuery, GetDeparturesQueryResponse>
    {
        private readonly IDeparturesRepository _departuresRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetDeparturesQueryHandler> _logger;

        public GetDeparturesQueryHandler(IDeparturesRepository departureRepository, IMapper mapper, ILogger<GetDeparturesQueryHandler> logger)
        {
            _departuresRepository = departureRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetDeparturesQueryResponse> Handle(GetDeparturesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetDeparturesQueryHandler - {request}");
            var items = await _departuresRepository.List(request.Count, request.Page);
            return new GetDeparturesQueryResponse{ Items = _mapper.Map<List<GetDepartureQueryResponse>>(items) };
        }
    }
}
