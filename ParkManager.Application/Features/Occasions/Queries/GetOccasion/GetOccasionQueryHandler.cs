using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Queries.GetOccasion
{
    public class GetOccasionQueryHandler : IRequestHandler<GetOccasionQuery, GetOccasionQueryResponse>
    {
        private readonly IOccasionsRepository _occasionsRepository;
        private readonly IMapper _mapper;

        public GetOccasionQueryHandler(IOccasionsRepository occasionRepository, IMapper mapper)
        {
            _occasionsRepository = occasionRepository;
            _mapper = mapper;
        }

        public async Task<GetOccasionQueryResponse> Handle(GetOccasionQuery request, CancellationToken cancellationToken)
        {
            var occassion = await _occasionsRepository.Get(request.Id);
            return _mapper.Map< GetOccasionQueryResponse>(occassion);
        }
    }
}