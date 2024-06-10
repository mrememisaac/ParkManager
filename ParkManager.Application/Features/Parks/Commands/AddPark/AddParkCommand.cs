using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Commands.AddPark
{
    public class AddParkCommand : IRequest<AddParkCommandResponse>
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
    }
}
