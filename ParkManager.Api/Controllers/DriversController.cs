using AutoMapper;
using Marvin.Cache.Headers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Drivers.Commands.AddDriver;
using ParkManager.Application.Features.Drivers.Commands.RemoveDriver;
using ParkManager.Application.Features.Drivers.Commands.UpdateDriver;
using ParkManager.Application.Features.Drivers.Queries.GetDriver;
using ParkManager.Application.Features.Drivers.Queries.GetDrivers;


namespace ParkManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<DriversController> _logger;

        public DriversController(IMediator mediator, IMapper mapper, ILogger<DriversController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetDriver")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 99999)]
        [HttpCacheValidation(MustRevalidate = true)]
        public async Task<ActionResult<GetDriverQueryResponse>> Get(Guid id)
        {
            _logger.BeginScope("GetDriver");
            var query = new GetDriverQuery(id);
            var commandResponse = await _mediator.Send(query);
            return commandResponse != null ? Ok(commandResponse) : NotFound();
        }

        [HttpGet(Name = "ListDrivers")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 99999)]
        [HttpCacheValidation(MustRevalidate = true)]
        public async Task<ActionResult<GetDriversQueryResponse>> List(int page = 0, int count = 100)
        {
            _logger.BeginScope("ListDrivers");
            var query = new GetDriversQuery(page, count);
            var commandResponse = await _mediator.Send(query);
            return Ok(commandResponse);
        }

        [HttpPost(Name = "AddDriver")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddDriverCommandResponse>> Post(Models.Driver driver)
        {
            _logger.BeginScope("AddDriver");
            var command = _mapper.Map<AddDriverCommand>(driver);
            var commandResponse = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = commandResponse.Id }, commandResponse);
        }

        [HttpPut(Name = "UpdateDriver")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Models.Driver driver)
        {
            _logger.BeginScope("UpdateDriver");
            var command = _mapper.Map<UpdateDriverCommand>(driver);
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteDriver")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            _logger.BeginScope("DeleteDriver");
            var command = new RemoveDriverCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
