using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Parks.Commands.RemovePark
{
    public class RemoveParkCommandHandler : IRequestHandler<RemoveParkCommand>
    {

        private readonly IMapper _mapper;
        private readonly IParksRepository _parkRepository;

        public RemoveParkCommandHandler(IParksRepository parkRepository, IMapper mapper)
        {
            _parkRepository = parkRepository;
            _mapper = mapper;
        }

        public async Task Handle(RemoveParkCommand request, CancellationToken cancellationToken)
        {
            await _parkRepository.Delete(request.Id);
        }
    }
}
