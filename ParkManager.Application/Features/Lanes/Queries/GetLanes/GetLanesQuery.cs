using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Lanes.Queries.GetLanes
{
    public record GetLanesQuery(int Page, int Count) : IRequest<GetLanesQueryResponse>
    { 
    }
}
