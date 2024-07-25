using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [Route("api/boards/{boardId}/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IToDoTasksServices _services;

        public TasksController(IToDoTasksServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasksAsync(int boardId)
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

        [HttpPut("{taskId}/update-title")]
        public async Task<ActionResult> UpdateTaskTitleAsync(int boardId, int taskId, string TaskTitle)
        {
            await _services.UpdateTaskTitleAsync(boardId, taskId, TaskTitle);
            return Ok();
        }

        [HttpPut("{taskId}/update-description")]
        public async Task<ActionResult> UpdateTaskDescriptionAsync(int boardId, int taskId, string TaskDescription)
        {
            await _services.UpdateTaskDescriptionAsync(boardId, taskId, TaskDescription);
            return Ok();
        }

        [HttpPut("{taskId}/update-status")]
        public async Task<ActionResult> UpdateTaskStatusAsync(int boardId, int taskId, int statusId)
        {
            await _services.UpdateTaskStatusAsync(boardId, taskId, statusId);
            return Ok();
        }

        [HttpPut("{taskId}/update-assignee")]
        public async Task<ActionResult> UpdateTaskAssigneeAsync(int boardId, int taskId, int assigneeId)
        {
            await _services.UpdateAssigneeAsync(boardId, taskId, assigneeId);
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
