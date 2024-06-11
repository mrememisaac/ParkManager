using AutoMapper;
using FluentValidation;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Slots.Commands.AddSlot
{
    public class AddSlotCommandHandler : IRequestHandler<AddSlotCommand, AddSlotCommandResponse>
    {
        private readonly ISlotsRepository _slotRepository;
        private readonly AddSlotCommandValidator _validator;
        private readonly IMapper _mapper;

        public AddSlotCommandHandler(ISlotsRepository slotRepository, AddSlotCommandValidator validator, IMapper mapper)
        {
            _slotRepository = slotRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<AddSlotCommandResponse> Handle(AddSlotCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var slot = _mapper.Map<Slot>(request);
            var response = await _slotRepository.Add(slot);
            return _mapper.Map<AddSlotCommandResponse>(response);
        }
    }
}
