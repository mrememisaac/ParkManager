using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Slots.Commands.AddSlot;
using ParkManager.Application.Features.Slots.Commands.RemoveSlot;
using ParkManager.Application.Features.Slots.Commands.UpdateSlot;
using ParkManager.Application.Features.Slots.Queries.GetSlot;
using ParkManager.Application.Features.Slots.Queries.GetSlots;


namespace ParkManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<SlotsController> _logger;
        public SlotsController(IMediator mediator, IMapper mapper, ILogger<SlotsController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetSlot")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetSlotQueryResponse>> Get(Guid id)
        {
            var query = new GetSlotQuery(id);
            var commandResponse = await _mediator.Send(query);
            return commandResponse != null ? Ok(commandResponse) : NotFound();
        }

        [HttpGet(Name = "ListSlots")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetSlotsQueryResponse>> List(int page = 0, int count = 100)
        {
            _logger.BeginScope("ListSlots");
            var query = new GetSlotsQuery(page, count);
            var commandResponse = await _mediator.Send(query);
            return Ok(commandResponse);
        }

        [HttpPost(Name = "AddSlot")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddSlotCommandResponse>> Post(Models.Slot slot)
        {
            _logger.BeginScope("AddSlot");
            var command = _mapper.Map<AddSlotCommand>(slot);
            var commandResponse = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = commandResponse.Id }, commandResponse);
        }

        [HttpPut(Name = "UpdateSlot")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Models.Slot slot)
        {
            _logger.BeginScope("UpdateSlot");
            var command = _mapper.Map<UpdateSlotCommand>(slot);
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteSlot")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            _logger.BeginScope("DeleteSlot");
            var command = new RemoveSlotCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
