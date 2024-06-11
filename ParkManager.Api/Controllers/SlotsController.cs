using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Features.Slots.Commands.AddSlot;
using ParkManager.Application.Features.Slots.Commands.RemoveSlot;
using ParkManager.Application.Features.Slots.Commands.UpdateSlot;
using ParkManager.Application.Features.Slots.Queries.GetSlot;

namespace ParkManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SlotsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<Models.Slot> Get(Guid id)
        {
            var query = new GetSlotQuery(id);
            var commandResponse = await _mediator.Send(query);
            var response = _mapper.Map<Models.Slot>(commandResponse);
            return response;
        }

        [HttpPost]
        public async Task<Models.Slot> Post(Models.Slot slot)
        {
            var command = _mapper.Map<AddSlotCommand>(slot);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Slot>(commandResponse);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<Models.Slot> Put(Guid id, [FromBody] Models.Slot slot)
        {
            var command = _mapper.Map<UpdateSlotCommand>(slot);
            var commandResponse = await _mediator.Send(command);
            var response = _mapper.Map<Models.Slot>(commandResponse);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var command = new RemoveSlotCommand(id);
            await _mediator.Send(command);
        }
    }    
}
