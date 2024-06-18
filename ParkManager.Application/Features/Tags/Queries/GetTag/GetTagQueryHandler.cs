using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;

namespace ParkManager.Application.Features.Tags.Queries.GetTag
{
    public class GetTagQueryHandler : IRequestHandler<GetTagQuery, GetTagQueryResponse>
    {
        private readonly ITagsRepository _tagsRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetTagQueryHandler> _logger;

        public GetTagQueryHandler(ITagsRepository tagRepository, IMapper mapper, ILogger<GetTagQueryHandler> logger)
        {
            _tagsRepository = tagRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetTagQueryResponse> Handle(GetTagQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving Tag with Id: {request.Id}");
            var response = await _tagsRepository.Get(request.Id);
            return _mapper.Map<GetTagQueryResponse>(response);
        }
    }
}
