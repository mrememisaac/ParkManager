using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Slots.Commands.AddSlot;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Slots.Commands.UpdateSlot
{
    public class UpdateSlotCommandHandler : IRequestHandler<UpdateSlotCommand, UpdateSlotCommandResponse>
    {
        private readonly ISlotsRepository _slotsRepository;
        private readonly UpdateSlotCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<UpdateSlotCommandHandler> _logger;

        public UpdateSlotCommandHandler(ISlotsRepository slotRepository, UpdateSlotCommandValidator validator, IMapper mapper, ILogger<UpdateSlotCommandHandler> logger)
        {
            _slotsRepository = slotRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateSlotCommandResponse> Handle(UpdateSlotCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling UpdateSlotCommand for {request.Id}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var slot = _mapper.Map<Slot>(request);
            var response = await _slotsRepository.Update(slot);
            return _mapper.Map<UpdateSlotCommandResponse>(response);
        }
    }
}
