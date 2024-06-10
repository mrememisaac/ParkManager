using AutoMapper;
using FluentValidation;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Tags.Commands.AddTag
{
    public class AddTagCommandHandler : IRequestHandler<AddTagCommand, AddTagCommandResponse>
    {
        private readonly ITagsRepository _tagsRepository;
        private readonly AddTagCommandValidator _validator;
        private readonly IMapper _mapper;

        public AddTagCommandHandler(ITagsRepository TagRepository, AddTagCommandValidator validator, IMapper mapper)
        {
            _tagsRepository = TagRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<AddTagCommandResponse> Handle(AddTagCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var tag = _mapper.Map<Tag>(request);
            var response = await _tagsRepository.Add(tag);
            return _mapper.Map<AddTagCommandResponse>(response);
        }
    }
}
