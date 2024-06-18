using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Slots.Commands.RemoveSlot
{
    public class RemoveSlotCommandHandler : IRequestHandler<RemoveSlotCommand>
    {

        private readonly IMapper _mapper; 
        private readonly ILogger<RemoveSlotCommandHandler> _logger;
        private readonly ISlotsRepository _slotRepository;

        public RemoveSlotCommandHandler(ISlotsRepository slotRepository, IMapper mapper, ILogger<RemoveSlotCommandHandler> logger)
        {
            _slotRepository = slotRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(RemoveSlotCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Removing Slot with Id: {request.Id}");
            await _slotRepository.Delete(request.Id);
        }
    }
}
