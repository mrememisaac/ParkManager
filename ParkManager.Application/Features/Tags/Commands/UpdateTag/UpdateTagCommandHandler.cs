using AutoMapper;
using FluentValidation;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Tags.Commands.UpdateTag
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, UpdateTagCommandResponse>
    {
        private readonly ITagsRepository _TagRepository;
        private readonly UpdateTagCommandValidator _validator;
        private readonly IMapper _mapper;

        public UpdateTagCommandHandler(ITagsRepository TagRepository, UpdateTagCommandValidator validator, IMapper mapper)
        {
            _TagRepository = TagRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateTagCommandResponse> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var tag = _mapper.Map<Tag>(request);
            var response = await _TagRepository.Update(tag);
            return _mapper.Map<UpdateTagCommandResponse>(response);
        }
    }
}
