using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Tags.Queries.GetTag;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Tags.Queries.GetTags
{
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, GetTagsQueryResponse>
    {
        private readonly ITagsRepository _tagsRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetTagsQueryHandler> _logger;

        public GetTagsQueryHandler(ITagsRepository tagRepository, IMapper mapper, ILogger<GetTagsQueryHandler> logger)
        {
            _tagsRepository = tagRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetTagsQueryResponse> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetTagsQueryHandler - Retrieving a set of tags {request}");
            var tags = await _tagsRepository.List(request.Count, request.Page);
            return new GetTagsQueryResponse { Items = _mapper.Map<List<GetTagQueryResponse>>(tags) };
        }
    }
}
