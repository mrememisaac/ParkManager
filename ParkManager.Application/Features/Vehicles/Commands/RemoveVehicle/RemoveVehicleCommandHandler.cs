using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Vehicles.Commands.RemoveVehicle
{
    public class RemoveVehicleCommandHandler : IRequestHandler<RemoveVehicleCommand>
    {

        private readonly IMapper _mapper;
        private readonly IVehiclesRepository _vehicleRepository;

        public RemoveVehicleCommandHandler(IVehiclesRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task Handle(RemoveVehicleCommand request, CancellationToken cancellationToken)
        {
            await _vehicleRepository.Delete(request.VehicleId);
        }
    }
}
