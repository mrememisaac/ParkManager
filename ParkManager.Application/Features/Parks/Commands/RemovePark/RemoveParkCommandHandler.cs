using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Parks.Commands.RemovePark
{
    public class RemoveParkCommandHandler : IRequestHandler<RemoveParkCommand>
    {

        private readonly IMapper _mapper; 
        private readonly ILogger<RemoveParkCommandHandler> _logger;
        private readonly IParksRepository _parkRepository;

        public RemoveParkCommandHandler(IParksRepository parkRepository, IMapper mapper, ILogger<RemoveParkCommandHandler> logger)
        {
            _parkRepository = parkRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(RemoveParkCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Removing Park: {request.Id}");
            await _parkRepository.Delete(request.Id);
        }
    }
}
