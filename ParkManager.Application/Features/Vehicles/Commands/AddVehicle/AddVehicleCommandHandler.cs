using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Commands.AddVehicle
{
    public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, AddVehicleCommandResponse>
    {
        private readonly IVehiclesRepository _vehicleRepository;
        private readonly AddVehicleCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<AddVehicleCommandHandler> _logger;

        public AddVehicleCommandHandler(IVehiclesRepository vehicleRepository, AddVehicleCommandValidator validator, IMapper mapper, ILogger<AddVehicleCommandHandler> logger)
        {
            _vehicleRepository = vehicleRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddVehicleCommandResponse> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling AddVehicleCommand for {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var vehicle = _mapper.Map<Vehicle>(request);
            var response = await _vehicleRepository.Add(vehicle);
            return _mapper.Map<AddVehicleCommandResponse>(response);
        }
    }
}
