using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Occasions.Commands.RemoveOccasion
{
    public class RemoveOccasionCommandHandler : IRequestHandler<RemoveOccasionCommand>
    {

        private readonly IMapper _mapper; 
        private readonly ILogger<RemoveOccasionCommandHandler> _logger;
        private readonly IOccasionsRepository _occasionRepository;

        public RemoveOccasionCommandHandler(IOccasionsRepository occasionRepository, IMapper mapper, ILogger<RemoveOccasionCommandHandler> logger)
        {
            _occasionRepository = occasionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(RemoveOccasionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Removing Occasion: {request.Id}");
            await _occasionRepository.Delete(request.Id);
        }
    }
}
