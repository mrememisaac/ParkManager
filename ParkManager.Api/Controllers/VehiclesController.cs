using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Vehicles.Commands.AddVehicle;
using ParkManager.Application.Features.Vehicles.Commands.RemoveVehicle;
using ParkManager.Application.Features.Vehicles.Commands.UpdateVehicle;
using ParkManager.Application.Features.Vehicles.Queries.GetVehicle;
using ParkManager.Application.Features.Vehicles.Queries.GetVehicles;


namespace ParkManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<VehiclesController> _logger;
        public VehiclesController(IMediator mediator, IMapper mapper, ILogger<VehiclesController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetVehicle")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetVehicleQueryResponse>> Get(Guid id)
        {
            _logger.BeginScope("GetVehicle");
            var query = new GetVehicleQuery(id);
            var commandResponse = await _mediator.Send(query);
            return commandResponse != null ? Ok(commandResponse) : NotFound();
        }

        [HttpGet(Name = "ListVehicles")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetVehiclesQueryResponse>> List(int page = 0, int count = 100)
        {
            _logger.BeginScope("ListVehicles");
            var query = new GetVehiclesQuery(page, count);
            var commandResponse = await _mediator.Send(query);
            return Ok(commandResponse);
        }

        [HttpPost(Name = "AddVehicle")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddVehicleCommandResponse>> Post(Models.Vehicle vehicle)
        {
            _logger.BeginScope("AddVehicle");
            var command = _mapper.Map<AddVehicleCommand>(vehicle);
            var commandResponse = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = commandResponse.Id }, commandResponse);
        }

        [HttpPut(Name ="UpdateVehicle")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Models.Vehicle vehicle)
        {
            _logger.BeginScope("UpdateVehicle");
            var command = _mapper.Map<UpdateVehicleCommand>(vehicle);
            await _mediator.Send(command);            
            return NoContent();
        }


        [HttpDelete("{id}", Name ="DeleteVehicle")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            _logger.BeginScope("DeleteVehicle");
            var command = new RemoveVehicleCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
