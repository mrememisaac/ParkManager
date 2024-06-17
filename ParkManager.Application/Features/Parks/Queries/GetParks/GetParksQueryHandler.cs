using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Parks.Queries.GetPark;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Queries.GetParks
{
    public class GetParksQueryHandler : IRequestHandler<GetParksQuery, GetParksQueryResponse>
    {
        private readonly IParksRepository _parksRepository;
        private readonly IMapper _mapper;

        public GetParksQueryHandler(IParksRepository parkRepository, IMapper mapper)
        {
            _parksRepository = parkRepository;
            _mapper = mapper;
        }

        public async Task<GetParksQueryResponse> Handle(GetParksQuery request, CancellationToken cancellationToken)
        {
            var parks = await _parksRepository.List(request.Count, request.Page);
            return new GetParksQueryResponse{ Items = _mapper.Map<List<GetParkQueryResponse>>(parks)};
        }
    }
}
