using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Tags.Commands.RemoveTag
{
    public class RemoveTagCommandHandler : IRequestHandler<RemoveTagCommand>
    {

        private readonly IMapper _mapper; 
        private readonly ILogger<RemoveTagCommandHandler> _logger;
        private readonly ITagsRepository _TagRepository;

        public RemoveTagCommandHandler(ITagsRepository TagRepository, IMapper mapper, ILogger<RemoveTagCommandHandler> logger)
        {
            _TagRepository = TagRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(RemoveTagCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Removing Tag: {request.TagId}");
            await _TagRepository.Delete(request.TagId);
        }
    }
}
