using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Vehicles.Commands.RemoveVehicle
{
    public class RemoveVehicleCommandHandler : IRequestHandler<RemoveVehicleCommand>
    {

        private readonly IMapper _mapper; 
        private readonly ILogger<RemoveVehicleCommandHandler> _logger;
        private readonly IVehiclesRepository _vehicleRepository;

        public RemoveVehicleCommandHandler(IVehiclesRepository vehicleRepository, IMapper mapper, ILogger<RemoveVehicleCommandHandler> logger)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(RemoveVehicleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Removing Vehicle with Id: {request.VehicleId}");
            await _vehicleRepository.Delete(request.VehicleId);
        }
    }
}
