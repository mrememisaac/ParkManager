using AutoMapper;
using FluentValidation;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Commands.AddOccasion
{
    public class AddOccasionCommandHandler : IRequestHandler<AddOccasionCommand, AddOccasionCommandResponse>
    {
        private readonly IOccasionsRepository _occasionRepository;
        private readonly AddOccasionCommandValidator _validator;
        private readonly IMapper _mapper;

        public AddOccasionCommandHandler(IOccasionsRepository occasionRepository, AddOccasionCommandValidator validator, IMapper mapper)
        {
            _occasionRepository = occasionRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<AddOccasionCommandResponse> Handle(AddOccasionCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var occasion = _mapper.Map<Occasion>(request);
            var response = await _occasionRepository.Add(occasion);
            return _mapper.Map<AddOccasionCommandResponse>(response);
        }
    }
}
