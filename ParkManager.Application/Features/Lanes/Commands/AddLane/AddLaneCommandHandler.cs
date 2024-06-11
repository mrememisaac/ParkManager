using AutoMapper;
using FluentValidation;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Lanes.Commands.AddLane
{
    public class AddLaneCommandHandler : IRequestHandler<AddLaneCommand, AddLaneCommandResponse>
    {
        private readonly ILanesRepository _laneRepository;
        private readonly AddLaneCommandValidator _validator;
        private readonly IMapper _mapper;

        public AddLaneCommandHandler(ILanesRepository laneRepository, AddLaneCommandValidator validator, IMapper mapper)
        {
            _laneRepository = laneRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<AddLaneCommandResponse> Handle(AddLaneCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var lane = _mapper.Map<Lane>(request);
            var response = await _laneRepository.Add(lane);
            return _mapper.Map<AddLaneCommandResponse>(response);
        }
    }
}
