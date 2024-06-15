using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Queries.GetArrivals
{
    public record GetArrivalsQuery(int Page, int Count) : IRequest<GetArrivalsQueryResponse>
    { 
    }
}
