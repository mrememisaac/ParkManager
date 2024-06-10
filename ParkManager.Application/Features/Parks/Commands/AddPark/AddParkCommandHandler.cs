using AutoMapper;
using FluentValidation;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Commands.AddPark
{
    public class AddParkCommandHandler : IRequestHandler<AddParkCommand, AddParkCommandResponse>
    {
        private readonly IParksRepository _parkRepository;
        private readonly AddParkCommandValidator _validator;
        private readonly IMapper _mapper;

        public AddParkCommandHandler(IParksRepository parkRepository, AddParkCommandValidator validator, IMapper mapper)
        {
            _parkRepository = parkRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<AddParkCommandResponse> Handle(AddParkCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var park = _mapper.Map<Park>(request);
            var response = await _parkRepository.Add(park);
            return _mapper.Map<AddParkCommandResponse>(response);
        }
    }
}
