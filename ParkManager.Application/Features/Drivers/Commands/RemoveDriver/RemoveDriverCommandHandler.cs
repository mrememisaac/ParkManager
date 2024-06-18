using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Drivers.Commands.RemoveDriver
{
    public class RemoveDriverCommandHandler : IRequestHandler<RemoveDriverCommand>
    {

        private readonly IMapper _mapper; 
        private readonly ILogger<RemoveDriverCommandHandler> _logger;
        private readonly IDriversRepository _driverRepository;

        public RemoveDriverCommandHandler(IDriversRepository driverRepository, IMapper mapper, ILogger<RemoveDriverCommandHandler> logger)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(RemoveDriverCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Removing Driver: {request.Id}");
            await _driverRepository.Delete(request.Id);
        }
    }
}
