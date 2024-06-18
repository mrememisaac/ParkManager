using AutoMapper;
using Marvin.Cache.Headers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Departures.Commands.AddDeparture;
using ParkManager.Application.Features.Departures.Commands.RemoveDeparture;
using ParkManager.Application.Features.Departures.Commands.UpdateDeparture;
using ParkManager.Application.Features.Departures.Queries.GetDeparture;
using ParkManager.Application.Features.Departures.Queries.GetDepartures;


namespace ParkManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeparturesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<DeparturesController> _logger;

        public DeparturesController(IMediator mediator, IMapper mapper, ILogger<DeparturesController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetDeparture")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 99999)]
        [HttpCacheValidation(MustRevalidate = true)]
        public async Task<ActionResult<GetDepartureQueryResponse>> Get(Guid id)
        {
            _logger.BeginScope("GetDeparture");
            var query = new GetDepartureQuery(id);
            var commandResponse = await _mediator.Send(query);
            return commandResponse != null ? Ok(commandResponse) : NotFound();
        }

        [HttpGet(Name = "ListDepartures")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 99999)]
        [HttpCacheValidation(MustRevalidate = true)]
        public async Task<ActionResult<GetDeparturesQueryResponse>> List(int page = 0, int count = 100)
        {
            _logger.BeginScope("ListDepartures");
            var query = new GetDeparturesQuery(page, count);
            var commandResponse = await _mediator.Send(query);
            return Ok(commandResponse);
        }

        [HttpPost(Name = "AddDeparture")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddDepartureCommandResponse>> Post(Models.Departure departure)
        {
            _logger.BeginScope("AddDeparture");
            var command = _mapper.Map<AddDepartureCommand>(departure);
            var commandResponse = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = commandResponse.Id }, commandResponse);
        }

        [HttpPut(Name = "UpdateDeparture")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Models.Departure departure)
        {
            _logger.BeginScope("UpdateDeparture");
            var command = _mapper.Map<UpdateDepartureCommand>(departure);
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteDeparture")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            _logger.BeginScope("DeleteDeparture");
            var command = new RemoveDepartureCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
