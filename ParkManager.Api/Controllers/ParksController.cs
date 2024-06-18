using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Parks.Commands.AddPark;
using ParkManager.Application.Features.Parks.Commands.RemovePark;
using ParkManager.Application.Features.Parks.Commands.UpdatePark;
using ParkManager.Application.Features.Parks.Queries.GetPark;
using ParkManager.Application.Features.Parks.Queries.GetParks;


namespace ParkManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ParksController> _logger;

        public ParksController(IMediator mediator, IMapper mapper, ILogger<ParksController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetPark")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetParkQueryResponse>> Get(Guid id)
        {
            _logger.BeginScope("GetPark");
            var query = new GetParkQuery(id);
            var commandResponse = await _mediator.Send(query);
            return commandResponse != null ? Ok(commandResponse) : NotFound();
        }

        [HttpGet(Name = "ListParks")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetParksQueryResponse>> List(int page = 0, int count = 100)
        {
            _logger.BeginScope("ListParks");
            var query = new GetParksQuery(page, count);
            var commandResponse = await _mediator.Send(query);
            return Ok(commandResponse);
        }

        [HttpPost(Name = "AddPark")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddParkCommandResponse>> Post(Models.Park park)
        {
            _logger.BeginScope("AddPark");
            var command = _mapper.Map<AddParkCommand>(park);
            var commandResponse = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = commandResponse.Id }, commandResponse);
        }

        [HttpPut(Name = "UpdatePark")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Models.Park park)
        {
            _logger.BeginScope("UpdatePark");
            var command = _mapper.Map<UpdateParkCommand>(park);
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeletePark")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            _logger.BeginScope("DeletePark");
            var command = new RemoveParkCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
