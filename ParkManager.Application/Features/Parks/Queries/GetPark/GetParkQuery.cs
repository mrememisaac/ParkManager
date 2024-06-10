using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Queries.GetPark
{
    public class GetParkQuery : IRequest<Park>
    {
        public int Id { get; set; }

        public GetParkQuery(int id)
        {
            Id = id;
        }
    }
}
