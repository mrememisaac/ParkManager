using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Tags.Queries.GetTag;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Tags.Queries.GetTags
{
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, GetTagsQueryResponse>
    {
        private readonly ITagsRepository _vehiclesRepository;
        private readonly IMapper _mapper;

        public GetTagsQueryHandler(ITagsRepository vehicleRepository, IMapper mapper)
        {
            _vehiclesRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<GetTagsQueryResponse> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehiclesRepository.List(request.Count, request.Page);
            return _mapper.Map<GetTagsQueryResponse>(vehicles);
        }
    }
}
