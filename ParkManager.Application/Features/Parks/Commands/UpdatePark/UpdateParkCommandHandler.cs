using AutoMapper;
using FluentValidation;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Parks.Commands.AddPark;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Commands.UpdatePark
{
    public class UpdateParkCommandHandler : IRequestHandler<UpdateParkCommand, UpdateParkCommandResponse>
    {
        private readonly IParksRepository _parksRepository;
        private readonly UpdateParkCommandValidator _validator;
        private readonly IMapper _mapper;

        public UpdateParkCommandHandler(IParksRepository parkRepository, UpdateParkCommandValidator validator, IMapper mapper)
        {
            _parksRepository = parkRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateParkCommandResponse> Handle(UpdateParkCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var park = _mapper.Map<Park>(request);
            var response = await _parksRepository.Update(park);
            return _mapper.Map<UpdateParkCommandResponse>(response);
        }
    }
}
