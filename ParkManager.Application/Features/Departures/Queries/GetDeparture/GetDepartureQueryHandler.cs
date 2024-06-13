using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Queries.GetDeparture
{
    public class GetDepartureQueryHandler : IRequestHandler<GetDepartureQuery, Departure>
    {
        private readonly IDeparturesRepository _departuresRepository;
        
        public GetDepartureQueryHandler(IDeparturesRepository repository)
        {
            _departuresRepository = repository;
        }

        public async Task<Departure> Handle(GetDepartureQuery request, CancellationToken cancellationToken)
        {
            return await _departuresRepository.Get(request.Id);
        }
    }
}
