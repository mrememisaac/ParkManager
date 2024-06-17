using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Drivers.Queries.GetDriver;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Drivers.Queries.GetDrivers
{
    public class GetDriversQueryHandler : IRequestHandler<GetDriversQuery, GetDriversQueryResponse>
    {
        private readonly IDriversRepository _driversRepository;
        private readonly IMapper _mapper;

        public GetDriversQueryHandler(IDriversRepository driverRepository, IMapper mapper)
        {
            _driversRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task<GetDriversQueryResponse> Handle(GetDriversQuery request, CancellationToken cancellationToken)
        {
            var drivers = await _driversRepository.List(request.Count, request.Page);
            return new GetDriversQueryResponse{ Items = _mapper.Map<List<GetDriverQueryResponse>>(drivers)};
        }
    }
}
