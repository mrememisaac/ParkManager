using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Queries.GetArrivals
{
    public class GetArrivalsQueryHandler : IRequestHandler<GetArrivalsQuery, GetArrivalsQueryResponse>
    {
        private readonly IArrivalsRepository _vehiclesRepository;
        private readonly IMapper _mapper;

        public GetArrivalsQueryHandler(IArrivalsRepository vehicleRepository, IMapper mapper)
        {
            _vehiclesRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<GetArrivalsQueryResponse> Handle(GetArrivalsQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehiclesRepository.List(request.Count, request.Page);
            return _mapper.Map<GetArrivalsQueryResponse>(vehicles);
        }
    }
}
