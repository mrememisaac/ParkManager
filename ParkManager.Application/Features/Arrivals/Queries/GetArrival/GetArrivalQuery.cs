using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Queries.GetArrival
{
    public class GetArrivalQuery : IRequest<GetArrivalQueryResponse>
    {
        public Guid Id { get; set; }

        public GetArrivalQuery(Guid id)
        {
            Id = id;
        }
    }
}
