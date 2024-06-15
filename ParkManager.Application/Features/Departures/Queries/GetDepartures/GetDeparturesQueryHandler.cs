using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Departures.Queries.GetDeparture;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Queries.GetDepartures
{
    public class GetDeparturesQueryHandler : IRequestHandler<GetDeparturesQuery, GetDeparturesQueryResponse>
    {
        private readonly IDeparturesRepository _vehiclesRepository;
        private readonly IMapper _mapper;

        public GetDeparturesQueryHandler(IDeparturesRepository vehicleRepository, IMapper mapper)
        {
            _vehiclesRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<GetDeparturesQueryResponse> Handle(GetDeparturesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehiclesRepository.List(request.Count, request.Page);
            return _mapper.Map<GetDeparturesQueryResponse>(vehicles);
        }
    }
}
