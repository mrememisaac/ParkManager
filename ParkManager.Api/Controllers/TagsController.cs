using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Tags.Commands.AddTag;
using ParkManager.Application.Features.Tags.Commands.RemoveTag;
using ParkManager.Application.Features.Tags.Commands.UpdateTag;
using ParkManager.Application.Features.Tags.Queries.GetTag;


namespace ParkManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TagsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<Models.Tag> Get(int id)
        {
            var query = new GetTagQuery(id);
            var commandResponse = await _mediator.Send(query);
            var response = _mapper.Map<Models.Tag>(commandResponse);
            return response;
        }

        [HttpPost]
        public async Task<Models.Tag> Post(Models.Tag tag)
        {
            var command = _mapper.Map<AddTagCommand>(tag);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Tag>(commandResponse);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<Models.Tag> Put(int id, [FromBody] Models.Tag tag)
        {
            var command = _mapper.Map<UpdateTagCommand>(tag);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Tag>(commandResponse);
            return response;
        }


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var command = new RemoveTagCommand(id);
            await _mediator.Send(command);
        }
    }
}
