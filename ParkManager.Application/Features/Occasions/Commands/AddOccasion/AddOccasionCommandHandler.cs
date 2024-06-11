using AutoMapper;
using FluentValidation;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Commands.AddOccasion
{
    public class AddOccasionCommandHandler : IRequestHandler<AddOccasionCommand, AddOccasionCommandResponse>
    {
        private readonly IOccasionsRepository _OccasionRepository;
        private readonly AddOccasionCommandValidator _validator;
        private readonly IMapper _mapper;

        public AddOccasionCommandHandler(IOccasionsRepository OccasionRepository, AddOccasionCommandValidator validator, IMapper mapper)
        {
            _OccasionRepository = OccasionRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<AddOccasionCommandResponse> Handle(AddOccasionCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var Occasion = _mapper.Map<Occasion>(request);
            var response = await _OccasionRepository.Add(Occasion);
            return _mapper.Map<AddOccasionCommandResponse>(response);
        }
    }
}
