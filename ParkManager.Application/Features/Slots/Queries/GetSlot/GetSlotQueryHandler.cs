using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Slots.Queries.GetSlot
{
    public class GetSlotQueryHandler : IRequestHandler<GetSlotQuery, GetSlotQueryResponse>
    {
        private readonly ISlotsRepository _repository;
        private readonly IMapper _mapper;

        public GetSlotQueryHandler(ISlotsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetSlotQueryResponse> Handle(GetSlotQuery request, CancellationToken cancellationToken)
        {
            var slot = await _repository.Get(request.Id);
            return _mapper.Map<GetSlotQueryResponse>(slot);
        }
    }
}