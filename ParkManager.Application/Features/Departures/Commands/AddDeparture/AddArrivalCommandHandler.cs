using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Commands.AddDeparture
{
    public class AddDepartureCommandHandler : IRequestHandler<AddDepartureCommand, AddDepartureCommandResponse>
    {
        private readonly IDeparturesRepository _departuresRepository;
        private readonly AddDepartureCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<AddDepartureCommandHandler> _logger;

        public AddDepartureCommandHandler(IDeparturesRepository driverRepository, AddDepartureCommandValidator validator, IMapper mapper, ILogger<AddDepartureCommandHandler> logger)
        {
            _departuresRepository = driverRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddDepartureCommandResponse> Handle(AddDepartureCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling AddDepartureCommand for {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var departure = _mapper.Map<Departure>(request);
            var response = await _departuresRepository.Add(departure);
            return _mapper.Map<AddDepartureCommandResponse>(response);
        }
    }
}
