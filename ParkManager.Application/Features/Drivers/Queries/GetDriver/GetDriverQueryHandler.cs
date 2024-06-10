using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Drivers.Queries.GetDriver
{
    public class GetDriverQueryHandler : IRequestHandler<GetDriverQuery, GetDriverQueryResponse>
    {
        private readonly IDriversRepository _driverRepository;
        private readonly IMapper _mapper;

        public GetDriverQueryHandler(IDriversRepository driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task<GetDriverQueryResponse> Handle(GetDriverQuery request, CancellationToken cancellationToken)
        {
            var response = await _driverRepository.Get(request.Id);
            return _mapper.Map<GetDriverQueryResponse>(response);
        }
    }
}
