using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Departures.Commands.RemoveDeparture
{
    public class RemoveDepartureCommandHandler : IRequestHandler<RemoveDepartureCommand>
    {

        private readonly IMapper _mapper;
        private readonly IDeparturesRepository _departuresRepository;

        public RemoveDepartureCommandHandler(IDeparturesRepository driverRepository, IMapper mapper)
        {
            _departuresRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task Handle(RemoveDepartureCommand request, CancellationToken cancellationToken)
        {
            await _departuresRepository.Delete(request.DepartureId);
        }
    }
}
