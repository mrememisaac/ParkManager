using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Parks.Queries.GetPark;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Queries.GetParks
{
    public class GetParksQueryHandler : IRequestHandler<GetParksQuery, GetParksQueryResponse>
    {
        private readonly IParksRepository _vehiclesRepository;
        private readonly IMapper _mapper;

        public GetParksQueryHandler(IParksRepository vehicleRepository, IMapper mapper)
        {
            _vehiclesRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<GetParksQueryResponse> Handle(GetParksQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehiclesRepository.List(request.Count, request.Page);
            return _mapper.Map<GetParksQueryResponse>(vehicles);
        }
    }
}
