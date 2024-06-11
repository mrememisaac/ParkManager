using AutoMapper;
using FluentValidation;
using MediatR;
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

        public UpdateSlotCommandHandler(ISlotsRepository slotRepository, UpdateSlotCommandValidator validator, IMapper mapper)
        {
            _slotsRepository = slotRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateSlotCommandResponse> Handle(UpdateSlotCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var slot = _mapper.Map<Slot>(request);
            var response = await _slotsRepository.Update(slot);
            return _mapper.Map<UpdateSlotCommandResponse>(response);
        }
    }
}
