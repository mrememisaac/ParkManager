using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Drivers.Commands.AddDriver;
using ParkManager.Application.Features.Drivers.Commands.RemoveDriver;
using ParkManager.Application.Features.Drivers.Commands.UpdateDriver;
using ParkManager.Application.Features.Drivers.Queries.GetDriver;


namespace ParkManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DriversController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<Models.Driver> Get(Guid id)
        {
            var query = new GetDriverQuery(id);
            var commandResponse = await _mediator.Send(query);
            var response = _mapper.Map<Models.Driver>(commandResponse);
            return response;
        }

        [HttpPost]
        public async Task<Models.Driver> Post(Models.Driver driver)
        {
            var command = _mapper.Map<AddDriverCommand>(driver);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Driver>(commandResponse);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<Models.Driver> Put(Guid id, [FromBody] Models.Driver driver)
        {
            var command = _mapper.Map<UpdateDriverCommand>(driver);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Driver>(commandResponse);
            return response;
        }


        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var command = new RemoveDriverCommand(id);
            await _mediator.Send(command);
        }
    }
}
