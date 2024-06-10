using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Parks.Commands.AddPark;
using ParkManager.Application.Features.Parks.Commands.RemovePark;
using ParkManager.Application.Features.Parks.Commands.UpdatePark;
using ParkManager.Application.Features.Parks.Queries.GetPark;


namespace ParkManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ParksController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<Models.Park> Get(int id)
        {
            var query = new GetParkQuery(id);
            var commandResponse = await _mediator.Send(query);
            var response = _mapper.Map<Models.Park>(commandResponse);
            return response;
        }

        [HttpPost]
        public async Task<Models.Park> Post(Models.Park park)
        {
            var command = _mapper.Map<AddParkCommand>(park);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Park>(commandResponse);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<Models.Park> Put(int id, [FromBody] Models.Park park)
        {
            var command = _mapper.Map<UpdateParkCommand>(park);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Park>(commandResponse);
            return response;
        }


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var command = new RemoveParkCommand(id);
            await _mediator.Send(command);
        }
    }
}
