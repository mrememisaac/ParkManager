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

        public VehiclesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetVehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetVehicleQueryResponse>> Get(Guid id)
        {
            var query = new GetVehicleQuery(id);
            var commandResponse = await _mediator.Send(query);
            return commandResponse != null ? Ok(commandResponse) : NotFound();
        }

        [HttpGet(Name = "ListVehicles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetVehiclesQueryResponse>> List(int page = 0, int count = 100)
        {
            var query = new GetVehiclesQuery(page, count);
            var commandResponse = await _mediator.Send(query);
            return Ok(commandResponse);
        }

        [HttpPost(Name = "AddVehicle")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AddVehicleCommandResponse>> Post(Models.Vehicle vehicle)
        {
            var command = _mapper.Map<AddVehicleCommand>(vehicle);
            var commandResponse = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = commandResponse.Id }, commandResponse);
        }

        [HttpPut(Name ="UpdateVehicle")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put([FromBody] Models.Vehicle vehicle)
        {
            var command = _mapper.Map<UpdateVehicleCommand>(vehicle);
            await _mediator.Send(command);            
            return NoContent();
        }


        [HttpDelete("{id}", Name ="DeleteVehicle")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new RemoveVehicleCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
