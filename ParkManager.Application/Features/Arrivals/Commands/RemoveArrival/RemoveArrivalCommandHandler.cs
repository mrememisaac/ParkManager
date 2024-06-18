using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Arrivals.Commands.RemoveArrival
{
    public class RemoveArrivalCommandHandler : IRequestHandler<RemoveArrivalCommand>
    {

        private readonly IMapper _mapper; 
        private readonly ILogger<RemoveArrivalCommandHandler> _logger;
        private readonly IArrivalsRepository _arrivalsRepository;

        public RemoveArrivalCommandHandler(IArrivalsRepository driverRepository, IMapper mapper, ILogger<RemoveArrivalCommandHandler> logger)
        {
            _arrivalsRepository = driverRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(RemoveArrivalCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Removing Arrival with Id: {request.ArrivalId}");
            await _arrivalsRepository.Delete(request.ArrivalId);
        }
    }
}
