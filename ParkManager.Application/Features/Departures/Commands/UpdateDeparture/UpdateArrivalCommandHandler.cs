using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Commands.UpdateDeparture
{
    public class UpdateDepartureCommandHandler : IRequestHandler<UpdateDepartureCommand, UpdateDepartureCommandResponse>
    {
        private readonly IDeparturesRepository _departuresRepository;
        private readonly UpdateDepartureCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<UpdateDepartureCommandHandler> _logger;

        public UpdateDepartureCommandHandler(IDeparturesRepository driverRepository, UpdateDepartureCommandValidator validator, IMapper mapper, ILogger<UpdateDepartureCommandHandler> logger)
        {
            _departuresRepository = driverRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateDepartureCommandResponse> Handle(UpdateDepartureCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling {nameof(UpdateDepartureCommand)} - {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var driver = _mapper.Map<Departure>(request);
            var response = await _departuresRepository.Update(driver);
            return _mapper.Map<UpdateDepartureCommandResponse>(response);
        }
    }
}
