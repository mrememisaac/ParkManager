using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Departures.Commands.RemoveDeparture
{
    public class RemoveDepartureCommandHandler : IRequestHandler<RemoveDepartureCommand>
    {

        private readonly IMapper _mapper; 
        private readonly ILogger<RemoveDepartureCommandHandler> _logger;
        private readonly IDeparturesRepository _departuresRepository;

        public RemoveDepartureCommandHandler(IDeparturesRepository driverRepository, IMapper mapper, ILogger<RemoveDepartureCommandHandler> logger)
        {
            _departuresRepository = driverRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(RemoveDepartureCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Removing Departure: {request}");
            await _departuresRepository.Delete(request.DepartureId);
        }
    }
}
