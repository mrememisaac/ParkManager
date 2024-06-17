using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Vehicles.Queries.GetVehicle;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Queries.GetVehicles
{
    public class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, GetVehiclesQueryResponse>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly IMapper _mapper;

        public GetVehiclesQueryHandler(IVehiclesRepository vehicleRepository, IMapper mapper)
        {
            _vehiclesRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<GetVehiclesQueryResponse> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehiclesRepository.List(request.Count, request.Page);
            return new GetVehiclesQueryResponse { Items = _mapper.Map<List<GetVehicleQueryResponse>>(vehicles) };
        }
    }
}
