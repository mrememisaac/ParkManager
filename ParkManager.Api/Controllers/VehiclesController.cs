using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Vehicles.Commands.AddVehicle;
using ParkManager.Application.Features.Vehicles.Commands.RemoveVehicle;
using ParkManager.Application.Features.Vehicles.Commands.UpdateVehicle;
using ParkManager.Application.Features.Vehicles.Queries.GetVehicle;


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

        [HttpGet("{id}")]
        public async Task<Models.Vehicle> Get(Guid id)
        {
            var query = new GetVehicleQuery(id);
            var commandResponse = await _mediator.Send(query);
            var response = _mapper.Map<Models.Vehicle>(commandResponse);
            return response;
        }

        [HttpPost]
        public async Task<Models.Vehicle> Post(Models.Vehicle vehicle)
        {
            var command = _mapper.Map<AddVehicleCommand>(vehicle);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Vehicle>(commandResponse);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<Models.Vehicle> Put(Guid id, [FromBody] Models.Vehicle vehicle)
        {
            var command = _mapper.Map<UpdateVehicleCommand>(vehicle);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Vehicle>(commandResponse);
            return response;
        }


        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var command = new RemoveVehicleCommand(id);
            await _mediator.Send(command);
        }
    }
}
