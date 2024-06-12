using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Queries.GetVehicle
{
    public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, GetVehicleQueryResponse>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly IMapper _mapper;

        public GetVehicleQueryHandler(IVehiclesRepository vehicleRepository, IMapper mapper)
        {
            _vehiclesRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<GetVehicleQueryResponse> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehiclesRepository.Get(request.Id);
            return _mapper.Map<GetVehicleQueryResponse>(vehicle);
        }
    }
}
