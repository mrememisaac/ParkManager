using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Slots.Commands.AddSlot
{
    public class AddSlotCommandHandler : IRequestHandler<AddSlotCommand, AddSlotCommandResponse>
    {
        private readonly ISlotsRepository _slotRepository;
        private readonly AddSlotCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<AddSlotCommandHandler> _logger;

        public AddSlotCommandHandler(ISlotsRepository slotRepository, AddSlotCommandValidator validator, IMapper mapper, ILogger<AddSlotCommandHandler> logger)
        {
            _slotRepository = slotRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddSlotCommandResponse> Handle(AddSlotCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling AddSlotCommand for {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var slot = _mapper.Map<Slot>(request);
            var response = await _slotRepository.Add(slot);
            return _mapper.Map<AddSlotCommandResponse>(response);
        }
    }
}
