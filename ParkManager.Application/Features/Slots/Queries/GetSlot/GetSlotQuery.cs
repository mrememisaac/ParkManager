using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Queries.GetPark
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
