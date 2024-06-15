using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Drivers.Queries.GetDriver;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Drivers.Queries.GetDrivers
{
    public class GetDriversQueryHandler : IRequestHandler<GetDriversQuery, GetDriversQueryResponse>
    {
        private readonly IDriversRepository _vehiclesRepository;
        private readonly IMapper _mapper;

        public GetDriversQueryHandler(IDriversRepository vehicleRepository, IMapper mapper)
        {
            _vehiclesRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<GetDriversQueryResponse> Handle(GetDriversQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehiclesRepository.List(request.Count, request.Page);
            return _mapper.Map<GetDriversQueryResponse>(vehicles);
        }
    }
}
