using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Occasions.Commands.RemoveOccasion
{
    public class RemoveOccasionCommandHandler : IRequestHandler<RemoveOccasionCommand>
    {

        private readonly IMapper _mapper;
        private readonly IOccasionsRepository _occasionRepository;

        public RemoveOccasionCommandHandler(IOccasionsRepository occasionRepository, IMapper mapper)
        {
            _occasionRepository = occasionRepository;
            _mapper = mapper;
        }

        public async Task Handle(RemoveOccasionCommand request, CancellationToken cancellationToken)
        {
            await _occasionRepository.Delete(request.Id);
        }
    }
}
