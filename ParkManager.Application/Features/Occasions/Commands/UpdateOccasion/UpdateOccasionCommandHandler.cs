using AutoMapper;
using FluentValidation;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Occasions.Commands.AddOccasion;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Commands.UpdateOccasion
{
    public class UpdateOccasionCommandHandler : IRequestHandler<UpdateOccasionCommand, UpdateOccasionCommandResponse>
    {
        private readonly IOccasionsRepository _occasionsRepository;
        private readonly UpdateOccasionCommandValidator _validator;
        private readonly IMapper _mapper;

        public UpdateOccasionCommandHandler(IOccasionsRepository occasionRepository, UpdateOccasionCommandValidator validator, IMapper mapper)
        {
            _occasionsRepository = occasionRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateOccasionCommandResponse> Handle(UpdateOccasionCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var occasion = _mapper.Map<Occasion>(request);
            var response = await _occasionsRepository.Update(occasion);
            return _mapper.Map<UpdateOccasionCommandResponse>(response);
        }
    }
}
