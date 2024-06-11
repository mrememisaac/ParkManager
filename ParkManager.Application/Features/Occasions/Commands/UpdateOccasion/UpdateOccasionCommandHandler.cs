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
        private readonly IOccasionsRepository _OccasionsRepository;
        private readonly UpdateOccasionCommandValidator _validator;
        private readonly IMapper _mapper;

        public UpdateOccasionCommandHandler(IOccasionsRepository OccasionRepository, UpdateOccasionCommandValidator validator, IMapper mapper)
        {
            _OccasionsRepository = OccasionRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateOccasionCommandResponse> Handle(UpdateOccasionCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var Occasion = _mapper.Map<Occasion>(request);
            var response = await _OccasionsRepository.Update(Occasion);
            return _mapper.Map<UpdateOccasionCommandResponse>(response);
        }
    }
}
