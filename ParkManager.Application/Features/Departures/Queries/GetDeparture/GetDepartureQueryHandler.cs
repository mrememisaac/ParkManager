using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Queries.GetDeparture
{
    public class GetDepartureQueryHandler : IRequestHandler<GetDepartureQuery, GetDepartureQueryResponse>
    {
        private readonly IDeparturesRepository _departuresRepository;
        private readonly IMapper _mapper;

        public GetDepartureQueryHandler(IDeparturesRepository repository, IMapper mapper)
        {
            _departuresRepository = repository;
            _mapper = mapper;
        }

        public async Task<GetDepartureQueryResponse> Handle(GetDepartureQuery request, CancellationToken cancellationToken)
        {
            var departure = await _departuresRepository.Get(request.Id);
            return _mapper.Map< GetDepartureQueryResponse>(departure);
        }
    }
}
