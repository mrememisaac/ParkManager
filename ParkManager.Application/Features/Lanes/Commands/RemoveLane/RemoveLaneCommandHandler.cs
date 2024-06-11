using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Lanes.Commands.RemoveLane
{
    public class RemoveLaneCommandHandler : IRequestHandler<RemoveLaneCommand>
    {

        private readonly IMapper _mapper;
        private readonly ILanesRepository _laneRepository;

        public RemoveLaneCommandHandler(ILanesRepository laneRepository, IMapper mapper)
        {
            _laneRepository = laneRepository;
            _mapper = mapper;
        }

        public async Task Handle(RemoveLaneCommand request, CancellationToken cancellationToken)
        {
            await _laneRepository.Delete(request.Id);
        }
    }
}
