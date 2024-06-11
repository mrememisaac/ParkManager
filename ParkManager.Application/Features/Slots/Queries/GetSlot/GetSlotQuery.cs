using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Slots.Queries.GetSlot
{
    public class GetSlotQuery : IRequest<Slot>
    {
        public Guid Id { get; set; }

        public GetSlotQuery(Guid id)
        {
            Id = id;
        }
    }
}
