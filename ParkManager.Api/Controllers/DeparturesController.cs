using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Departures.Commands.AddDeparture;
using ParkManager.Application.Features.Departures.Commands.RemoveDeparture;
using ParkManager.Application.Features.Departures.Commands.UpdateDeparture;
using ParkManager.Application.Features.Departures.Queries.GetDeparture;

namespace ParkManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeparturesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DeparturesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<Models.Departure> Get(Guid id)
        {
            var query = new GetDepartureQuery(id);
            var commandResponse = await _mediator.Send(query);
            var response = _mapper.Map<Models.Departure>(commandResponse);
            return response;
        }

        [HttpPost]
        public async Task<Models.Departure> Post(Models.Departure departure)
        {
            var command = _mapper.Map<AddDepartureCommand>(departure);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Departure>(commandResponse);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<Models.Departure> Put(Guid id, [FromBody] Models.Departure departure)
        {
            var command = _mapper.Map<UpdateDepartureCommand>(departure);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Departure>(commandResponse);
            return response;
        }


        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var command = new RemoveDepartureCommand(id);
            await _mediator.Send(command);
        }
    }
}
