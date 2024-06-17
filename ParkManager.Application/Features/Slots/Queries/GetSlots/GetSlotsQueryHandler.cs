using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Slots.Queries.GetSlot;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Slots.Queries.GetSlots
{
    public class GetSlotsQueryHandler : IRequestHandler<GetSlotsQuery, GetSlotsQueryResponse>
    {
        private readonly ISlotsRepository _slotsRepository;
        private readonly IMapper _mapper;

        public GetSlotsQueryHandler(ISlotsRepository slotRepository, IMapper mapper)
        {
            _slotsRepository = slotRepository;
            _mapper = mapper;
        }

        public async Task<GetSlotsQueryResponse> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            var slots = await _slotsRepository.List(request.Count, request.Page);
            return new GetSlotsQueryResponse{ Items =_mapper.Map<List<GetSlotQueryResponse>>(slots) };
        }
    }
}
