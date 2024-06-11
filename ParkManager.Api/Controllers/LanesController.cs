using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Lanes.Commands.AddLane;
using ParkManager.Application.Features.Lanes.Commands.RemoveLane;
using ParkManager.Application.Features.Lanes.Commands.UpdateLane;
using ParkManager.Application.Features.Lanes.Queries.GetLane;
using System;
using System.Threading.Tasks;

namespace ParkManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LanesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<Models.Lane> Get(Guid id)
        {
            var query = new GetLaneQuery(id);
            var commandResponse = await _mediator.Send(query);
            var response = _mapper.Map<Models.Lane>(commandResponse);
            return response;
        }

        [HttpPost]
        public async Task<Models.Lane> Post(Models.Lane lane)
        {
            var command = _mapper.Map<AddLaneCommand>(lane);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Lane>(commandResponse);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<Models.Lane> Put(Guid id, [FromBody] Models.Lane lane)
        {
            var command = _mapper.Map<UpdateLaneCommand>(lane);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Lane>(commandResponse);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var command = new RemoveLaneCommand(id);
            await _mediator.Send(command);
        }
    }
}
