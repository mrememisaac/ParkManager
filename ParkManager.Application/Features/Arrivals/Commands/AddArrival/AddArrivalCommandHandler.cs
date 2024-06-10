using AutoMapper;
using FluentValidation;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Commands.AddArrival
{
    public class AddArrivalCommandHandler : IRequestHandler<AddArrivalCommand, AddArrivalCommandResponse>
    {
        private readonly IArrivalsRepository _arrivalsRepository;
        private readonly AddArrivalCommandValidator _validator;
        private readonly IMapper _mapper;

        public AddArrivalCommandHandler(IArrivalsRepository driverRepository, AddArrivalCommandValidator validator, IMapper mapper)
        {
            _arrivalsRepository = driverRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<AddArrivalCommandResponse> Handle(AddArrivalCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var arrival = _mapper.Map<Arrival>(request);
            var response = await _arrivalsRepository.Add(arrival);
            return _mapper.Map<AddArrivalCommandResponse>(response);
        }
    }
}
