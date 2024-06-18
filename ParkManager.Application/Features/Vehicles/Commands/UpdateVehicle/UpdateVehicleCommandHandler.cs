using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, UpdateVehicleCommandResponse>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly UpdateVehicleCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<UpdateVehicleCommandHandler> _logger;

        public UpdateVehicleCommandHandler(IVehiclesRepository vehicleRepository, UpdateVehicleCommandValidator validator, IMapper mapper, ILogger<UpdateVehicleCommandHandler> logger)
        {
            _vehiclesRepository = vehicleRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateVehicleCommandResponse> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating Vehicle: {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var vehicle = _mapper.Map<Vehicle>(request);
            var response = await _vehiclesRepository.Update(vehicle);
            return _mapper.Map<UpdateVehicleCommandResponse>(response);
        }
    }
}
