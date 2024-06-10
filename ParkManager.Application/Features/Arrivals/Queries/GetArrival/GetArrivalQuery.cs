using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Queries.GetArrival
{
    public class GetArrivalQuery : IRequest<Arrival>
    {
        public int Id { get; set; }

        public GetArrivalQuery(int id)
        {
            Id = id;
        }
    }
}
