using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Occasions.Queries.GetOccasion;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Queries.GetOccasions
{
    public class GetOccasionsQueryHandler : IRequestHandler<GetOccasionsQuery, GetOccasionsQueryResponse>
    {
        private readonly IOccasionsRepository _occasionsRepository;
        private readonly IMapper _mapper;

        public GetOccasionsQueryHandler(IOccasionsRepository occasionRepository, IMapper mapper)
        {
            _occasionsRepository = occasionRepository;
            _mapper = mapper;
        }

        public async Task<GetOccasionsQueryResponse> Handle(GetOccasionsQuery request, CancellationToken cancellationToken)
        {
            var occasions = await _occasionsRepository.List(request.Count, request.Page);
            return new GetOccasionsQueryResponse { Items = _mapper.Map<List<GetOccasionQueryResponse>>(occasions) };
        }
    }
}
