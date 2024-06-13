using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Commands.AddPark
{
    public class AddParkCommand : IRequest<AddParkCommandResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
