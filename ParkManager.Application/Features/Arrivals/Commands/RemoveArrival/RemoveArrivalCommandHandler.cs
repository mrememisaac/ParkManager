using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Arrivals.Commands.RemoveArrival
{
    public class RemoveArrivalCommandHandler : IRequestHandler<RemoveArrivalCommand>
    {

        private readonly IMapper _mapper;
        private readonly IArrivalsRepository _arrivalsRepository;

        public RemoveArrivalCommandHandler(IArrivalsRepository driverRepository, IMapper mapper)
        {
            _arrivalsRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task Handle(RemoveArrivalCommand request, CancellationToken cancellationToken)
        {
            await _arrivalsRepository.Delete(request.ArrivalId);
        }
    }
}
