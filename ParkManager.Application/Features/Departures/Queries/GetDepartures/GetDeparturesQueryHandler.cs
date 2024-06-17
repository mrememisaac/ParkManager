using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Departures.Queries.GetDeparture;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Queries.GetDepartures
{
    public class GetDeparturesQueryHandler : IRequestHandler<GetDeparturesQuery, GetDeparturesQueryResponse>
    {
        private readonly IDeparturesRepository _departuresRepository;
        private readonly IMapper _mapper;

        public GetDeparturesQueryHandler(IDeparturesRepository departureRepository, IMapper mapper)
        {
            _departuresRepository = departureRepository;
            _mapper = mapper;
        }

        public async Task<GetDeparturesQueryResponse> Handle(GetDeparturesQuery request, CancellationToken cancellationToken)
        {
            var items = await _departuresRepository.List(request.Count, request.Page);
            return new GetDeparturesQueryResponse{ Items = _mapper.Map<List<GetDepartureQueryResponse>>(items) };
        }
    }
}
