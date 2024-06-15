using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Queries.GetParks
{
    public record GetParksQuery(int Page, int Count) : IRequest<GetParksQueryResponse>
    { 
    }
}
