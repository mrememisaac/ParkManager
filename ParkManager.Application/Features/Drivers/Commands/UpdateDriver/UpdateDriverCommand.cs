using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Drivers.Commands.UpdateDriver
{
    public class UpdateDriverCommand : IRequest<UpdateDriverCommandResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
