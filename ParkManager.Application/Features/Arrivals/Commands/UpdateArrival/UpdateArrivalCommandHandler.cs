using AutoMapper;
using FluentValidation;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Commands.UpdateArrival
{
    public class UpdateArrivalCommandHandler : IRequestHandler<UpdateArrivalCommand, UpdateArrivalCommandResponse>
    {
        private readonly IArrivalsRepository _arrivalsRepository;
        private readonly UpdateArrivalCommandValidator _validator;
        private readonly IMapper _mapper;

        public UpdateArrivalCommandHandler(IArrivalsRepository driverRepository, UpdateArrivalCommandValidator validator, IMapper mapper)
        {
            _arrivalsRepository = driverRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateArrivalCommandResponse> Handle(UpdateArrivalCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var driver = _mapper.Map<Arrival>(request);
            var response = await _arrivalsRepository.Update(driver);
            return _mapper.Map<UpdateArrivalCommandResponse>(response);
        }
    }
}
