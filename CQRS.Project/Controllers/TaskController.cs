using CQRS.Application.DTOs;
using CQRS.Infrastructure.Commands;
using CQRS.Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        public async Task<IEnumerable<TaskItemDto>> GetAll()
            => await _mediator.Send(new GetAllTasksQuery());

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetById(int id)
        {
            var query = new GetTaskByIdQuery(id);
            var taskItem = await _mediator.Send(query);

            if (taskItem is null)
                return NotFound();

            return taskItem;
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> Create(CreateTaskCommand command)
        {
            var taskItem = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = taskItem.Id }, taskItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItemDto>> Update(int id, UpdateTaskCommand command)
        {
            if (command.Id != id)
                return BadRequest("The id's are different");

            var taskItem = await _mediator.Send(command);
            if (taskItem is null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteTaskCommand(id));
            if (!result) return NotFound();

            return NoContent();
        }
    }    
}
