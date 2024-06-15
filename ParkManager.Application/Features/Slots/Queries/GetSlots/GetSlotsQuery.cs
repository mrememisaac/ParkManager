using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Slots.Queries.GetSlots
{
    public record GetSlotsQuery(int Page, int Count) : IRequest<GetSlotsQueryResponse>
    { 
    }
}
