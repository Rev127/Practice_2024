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

        [HttpPut("{taskId}/title")]
        public async Task<ActionResult> UpdateTaskTitleAsync(int taskId, UpdateTaskDto updateTaskDto)
        {
            await _services.UpdateTaskTitleAsync(taskId, updateTaskDto);
            return Ok();
        }

        [HttpPut("{taskId}/description")]
        public async Task<ActionResult> UpdateTaskDescriptionAsync(int taskId, UpdateTaskDto updateTaskDto)
        {
            await _services.UpdateTaskDescriptionAsync(taskId, updateTaskDto);
            return Ok();
        }

        [HttpPut("{taskId}/status")]
        public async Task<ActionResult> UpdateTaskStatusAsync(int taskId, UpdateTaskDto updateTaskDto)
        {
            await _services.UpdateTaskStatusAsync(taskId, updateTaskDto);
            return Ok();
        }

        [HttpPut("{taskId}/assignee")]
        public async Task<ActionResult> UpdateTaskAssigneeAsync(int taskId, UpdateTaskDto updateTaskDto)
        {
            await _services.UpdateAssigneeAsync(taskId, updateTaskDto);
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
