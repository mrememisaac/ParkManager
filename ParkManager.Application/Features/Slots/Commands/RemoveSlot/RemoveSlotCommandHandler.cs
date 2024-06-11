using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Slots.Commands.RemoveSlot
{
    public class RemoveSlotCommandHandler : IRequestHandler<RemoveSlotCommand>
    {

        private readonly IMapper _mapper;
        private readonly ISlotsRepository _slotRepository;

        public RemoveSlotCommandHandler(ISlotsRepository slotRepository, IMapper mapper)
        {
            _slotRepository = slotRepository;
            _mapper = mapper;
        }

        public async Task Handle(RemoveSlotCommand request, CancellationToken cancellationToken)
        {
            await _slotRepository.Delete(request.Id);
        }
    }
}
