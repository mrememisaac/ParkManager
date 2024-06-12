using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Tags.Queries.GetTag
{
    public class GetTagQueryHandler : IRequestHandler<GetTagQuery, GetTagQueryResponse>
    {
        private readonly ITagsRepository _tagsRepository;
        private readonly IMapper _mapper;

        public GetTagQueryHandler(ITagsRepository tagRepository, IMapper mapper)
        {
            _tagsRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<GetTagQueryResponse> Handle(GetTagQuery request, CancellationToken cancellationToken)
        {
            var response = await _tagsRepository.Get(request.Id);
            return _mapper.Map<GetTagQueryResponse>(response);
        }
    }
}
