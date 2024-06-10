using AutoMapper;
using FluentValidation;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Commands.AddVehicle
{
    public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, AddVehicleCommandResponse>
    {
        private readonly IVehiclesRepository _vehicleRepository;
        private readonly AddVehicleCommandValidator _validator;
        private readonly IMapper _mapper;

        public AddVehicleCommandHandler(IVehiclesRepository vehicleRepository, AddVehicleCommandValidator validator, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<AddVehicleCommandResponse> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var vehicle = _mapper.Map<Vehicle>(request);
            var response = await _vehicleRepository.Add(vehicle);
            return _mapper.Map<AddVehicleCommandResponse>(response);
        }
    }
}
