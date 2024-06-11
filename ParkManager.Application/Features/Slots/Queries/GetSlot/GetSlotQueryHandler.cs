using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Slots.Queries.GetSlot
{
    public class GetSlotQueryHandler : IRequestHandler<GetSlotQuery, Slot>
    {
        private readonly ISlotsRepository _repository;
        
        public GetSlotQueryHandler(ISlotsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Slot> Handle(GetSlotQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }
    }
}