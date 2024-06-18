using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Lanes.Commands.RemoveLane
{
    public class RemoveLaneCommandHandler : IRequestHandler<RemoveLaneCommand>
    {

        private readonly IMapper _mapper; 
        private readonly ILogger<RemoveLaneCommandHandler> _logger;
        private readonly ILanesRepository _laneRepository;

        public RemoveLaneCommandHandler(ILanesRepository laneRepository, IMapper mapper, ILogger<RemoveLaneCommandHandler> logger)
        {
            _laneRepository = laneRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(RemoveLaneCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Removing Lane with Id: {request.Id}");
            await _laneRepository.Delete(request.Id);
        }
    }
}
