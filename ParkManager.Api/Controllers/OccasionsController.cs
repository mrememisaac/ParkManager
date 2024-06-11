using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Occasions.Commands.AddOccasion;
using ParkManager.Application.Features.Occasions.Commands.RemoveOccasion;
using ParkManager.Application.Features.Occasions.Commands.UpdateOccasion;
using ParkManager.Application.Features.Occasions.Queries.GetOccasion;


namespace ParkManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccasionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OccasionsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<Models.Occasion> Get(Guid id)
        {
            var query = new GetOccasionQuery(id);
            var commandResponse = await _mediator.Send(query);
            var response = _mapper.Map<Models.Occasion>(commandResponse);
            return response;
        }

        [HttpPost]
        public async Task<Models.Occasion> Post(Models.Occasion vehicle)
        {
            var command = _mapper.Map<AddOccasionCommand>(vehicle);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Occasion>(commandResponse);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<Models.Occasion> Put(Guid id, [FromBody] Models.Occasion vehicle)
        {
            var command = _mapper.Map<UpdateOccasionCommand>(vehicle);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Occasion>(commandResponse);
            return response;
        }


        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var command = new RemoveOccasionCommand(id);
            await _mediator.Send(command);
        }
    }
}
