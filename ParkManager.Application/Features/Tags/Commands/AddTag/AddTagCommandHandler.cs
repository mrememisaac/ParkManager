using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Tags.Commands.AddTag
{
    public class AddTagCommandHandler : IRequestHandler<AddTagCommand, AddTagCommandResponse>
    {
        private readonly ITagsRepository _tagsRepository;
        private readonly AddTagCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<AddTagCommandHandler> _logger;

        public AddTagCommandHandler(ITagsRepository TagRepository, AddTagCommandValidator validator, IMapper mapper, ILogger<AddTagCommandHandler> logger)
        {
            _tagsRepository = TagRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddTagCommandResponse> Handle(AddTagCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling AddTagCommand for {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var tag = _mapper.Map<Tag>(request);
            var response = await _tagsRepository.Add(tag);
            return _mapper.Map<AddTagCommandResponse>(response);
        }
    }
}
