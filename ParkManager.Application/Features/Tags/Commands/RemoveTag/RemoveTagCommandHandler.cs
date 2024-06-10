using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Tags.Commands.RemoveTag
{
    public class RemoveTagCommandHandler : IRequestHandler<RemoveTagCommand>
    {

        private readonly IMapper _mapper;
        private readonly ITagsRepository _TagRepository;

        public RemoveTagCommandHandler(ITagsRepository TagRepository, IMapper mapper)
        {
            _TagRepository = TagRepository;
            _mapper = mapper;
        }

        public async Task Handle(RemoveTagCommand request, CancellationToken cancellationToken)
        {
            await _TagRepository.Delete(request.TagId);
        }
    }
}
