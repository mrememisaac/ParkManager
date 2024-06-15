using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Queries.GetOccasions
{
    public record GetOccasionsQuery(int Page, int Count) : IRequest<GetOccasionsQueryResponse>
    { 
    }
}
