﻿using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Tags.Queries.GetTag;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Tags.Queries.GetTags
{
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, GetTagsQueryResponse>
    {
        private readonly ITagsRepository _tagsRepository;
        private readonly IMapper _mapper;

        public GetTagsQueryHandler(ITagsRepository tagRepository, IMapper mapper)
        {
            _tagsRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<GetTagsQueryResponse> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await _tagsRepository.List(request.Count, request.Page);
            return new GetTagsQueryResponse { Items = _mapper.Map<List<GetTagQueryResponse>>(tags) };
        }
    }
}
