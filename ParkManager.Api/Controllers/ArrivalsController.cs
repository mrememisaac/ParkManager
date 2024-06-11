using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Arrivals.Commands.AddArrival;
using ParkManager.Application.Features.Arrivals.Commands.RemoveArrival;
using ParkManager.Application.Features.Arrivals.Commands.UpdateArrival;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;


namespace ParkManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArrivalsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ArrivalsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<Models.Arrival> Get(Guid id)
        {
            var query = new GetArrivalQuery(id);
            var commandResponse = await _mediator.Send(query);
            var response = _mapper.Map<Models.Arrival>(commandResponse);
            return response;
        }

        [HttpPost]
        public async Task<Models.Arrival> Post(Models.Arrival arrival)
        {
            var command = _mapper.Map<AddArrivalCommand>(arrival);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Arrival>(commandResponse);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<Models.Arrival> Put(Guid id, [FromBody] Models.Arrival arrival)
        {
            var command = _mapper.Map<UpdateArrivalCommand>(arrival);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Arrival>(commandResponse);
            return response;
        }


        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var command = new RemoveArrivalCommand(id);
            await _mediator.Send(command);
        }
    }
}
