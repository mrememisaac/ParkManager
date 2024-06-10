using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Queries.GetVehicle
{
    public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, Vehicle>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        
        public GetVehicleQueryHandler(IVehiclesRepository vehicleRepository)
        {
            _vehiclesRepository = vehicleRepository;
        }

        public async Task<Vehicle> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
        {
            return await _vehiclesRepository.Get(request.Id);
        }
    }
}
