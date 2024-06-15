using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Queries.GetPark
{
    public class GetParkQueryHandler : IRequestHandler<GetParkQuery, GetParkQueryResponse>
    {
        private readonly IParksRepository _parksRepository;
        private readonly IMapper _mapper;

        public GetParkQueryHandler(IParksRepository parkRepository, IMapper mapper)
        {
            _parksRepository = parkRepository;
            _mapper = mapper;
        }

        public async Task<GetParkQueryResponse> Handle(GetParkQuery request, CancellationToken cancellationToken)
        {
            var park = await _parksRepository.Get(request.Id);
            return _mapper.Map< GetParkQueryResponse>(park);
        }
    }
}