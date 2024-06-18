using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Commands.UpdateArrival
{
    public class UpdateArrivalCommandHandler : IRequestHandler<UpdateArrivalCommand, UpdateArrivalCommandResponse>
    {
        private readonly IArrivalsRepository _arrivalsRepository;
        private readonly UpdateArrivalCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<UpdateArrivalCommandHandler> _logger;

        public UpdateArrivalCommandHandler(IArrivalsRepository driverRepository, UpdateArrivalCommandValidator validator, IMapper mapper, ILogger<UpdateArrivalCommandHandler> logger)
        {
            _arrivalsRepository = driverRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateArrivalCommandResponse> Handle(UpdateArrivalCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling {nameof(UpdateArrivalCommand)} - {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var driver = _mapper.Map<Arrival>(request);
            var response = await _arrivalsRepository.Update(driver);
            return _mapper.Map<UpdateArrivalCommandResponse>(response);
        }
    }
}
