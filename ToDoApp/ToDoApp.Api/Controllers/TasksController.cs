using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [Route("api/to-do-tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IToDoTasksServices _services;

        public TasksController(IToDoTasksServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasksAsync()
        {
            var tasks = await _services.GetTasksAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTaskAsync(CreateTaskDto taskDto)
        {
            await _services.CreateTaskAsync(taskDto);
            return Ok();
        }

        [HttpPut("update-task-title")]
        public async Task<ActionResult> UpdateTaskTitleAsync(int TaskId, string TaskTitle)
        {
            await _services.UpdateTaskTitleAsync(TaskId, TaskTitle);
            return Ok();
        }

        [HttpPut("update-task-description")]
        public async Task<ActionResult> UpdateTaskDescriptionAsync(int TaskId, string TaskDescription)
        {
            await _services.UpdateTaskDescriptionAsync(TaskId, TaskDescription);
            return Ok();
        }

        [HttpPut("update-task-status")]
        public async Task<ActionResult> UpdateTaskStatusAsync(int TaskId, int statusId)
        {
            await _services.UpdateTaskStatusAsync(TaskId, statusId);
            return Ok();
        }

        [HttpPut("update-task-assignee")]
        public async Task<ActionResult> UpdateTaskAssigneeAsync(int TaskId, int assigneeId)
        {
            await _services.UpdateAssigneeAsync(TaskId, assigneeId);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTaskAsync(int TaskId)
        {
            await _services.DeleteTaskAsync(TaskId);
            return Ok();
        }
    }
}
