using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [Authorize]
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IToDoTasksServices _services;

        public TasksController(IToDoTasksServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTaskDto>>> GetTasksAsync(int boardId)
        {
            var tasks = await _services.GetTasksAsync(boardId);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTaskAsync(int boardId, CreateTaskDto taskDto)
        {
            await _services.CreateTaskAsync(boardId, taskDto);
            return Ok();
        }

        [HttpPut("/title")]
        public async Task<ActionResult> UpdateTaskTitleAsync(UpdateTaskDto updateTaskDto)
        {
            await _services.UpdateTaskTitleAsync(updateTaskDto);
            return Ok();
        }

        [HttpPut("/description")]
        public async Task<ActionResult> UpdateTaskDescriptionAsync(UpdateTaskDto updateTaskDto)
        {
            await _services.UpdateTaskDescriptionAsync(updateTaskDto);
            return Ok();
        }

        [HttpPut("/status")]
        public async Task<ActionResult> UpdateTaskStatusAsync(UpdateTaskDto updateTaskDto)
        {
            await _services.UpdateTaskStatusAsync(updateTaskDto);
            return Ok();
        }

        [HttpPut("/assignee")]
        public async Task<ActionResult> UpdateTaskAssigneeAsync(UpdateTaskDto updateTaskDto)
        {
            await _services.UpdateAssigneeAsync(updateTaskDto);
            return Ok();
        }

        [HttpDelete("{taskId}")]
        public async Task<ActionResult> DeleteTaskAsync(int boardId, int taskId)
        {
            await _services.DeleteTaskAsync(boardId, taskId);
            return Ok();
        }
    }
}
