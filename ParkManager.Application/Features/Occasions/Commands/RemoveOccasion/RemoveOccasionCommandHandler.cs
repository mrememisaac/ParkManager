using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Occasions.Commands.RemoveOccasion
{
    public class RemoveOccasionCommandHandler : IRequestHandler<RemoveOccasionCommand>
    {

        private readonly IMapper _mapper;
        private readonly IOccasionsRepository _OccasionRepository;

        public RemoveOccasionCommandHandler(IOccasionsRepository OccasionRepository, IMapper mapper)
        {
            _OccasionRepository = OccasionRepository;
            _mapper = mapper;
        }

        public async Task Handle(RemoveOccasionCommand request, CancellationToken cancellationToken)
        {
            await _OccasionRepository.Delete(request.Id);
        }
    }
}
