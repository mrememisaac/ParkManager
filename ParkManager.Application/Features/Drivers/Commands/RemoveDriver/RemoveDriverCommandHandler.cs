using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Drivers.Commands.RemoveDriver
{
    public class RemoveDriverCommandHandler : IRequestHandler<RemoveDriverCommand>
    {

        private readonly IMapper _mapper;
        private readonly IDriversRepository _driverRepository;

        public RemoveDriverCommandHandler(IDriversRepository driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task Handle(RemoveDriverCommand request, CancellationToken cancellationToken)
        {
            await _driverRepository.Delete(request.Id);
        }
    }
}
