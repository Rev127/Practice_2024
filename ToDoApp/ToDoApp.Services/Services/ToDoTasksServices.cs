using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Enums;
using ToDoApp.Services.Exceptions;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services
{
    public class ToDoTasksServices : IToDoTasksServices
    {
        private readonly ToDoContext _context;
        private readonly ICurrentUserServices _currentUserServices;

        public ToDoTasksServices(ToDoContext context, ICurrentUserServices currentUserServices) 
        {
            _context = context;
            _currentUserServices = currentUserServices;
        }


        public async Task<List<Tasks>> GetTasksAsync(int boardId)
        {
            /*var board = await _context.Board.FindAsync(boardId);

            if (board is null)
            {
                throw new BoardNotFoundException();
            }*/

            return await _context.Task.Where(x => x.AssigneeId == _currentUserServices.UserId && x.BoardId == boardId).ToListAsync();
        }

        public async Task CreateTaskAsync(int boardId, CreateTaskDto taskDto)
        {
            var board = await _context.Board.FindAsync(boardId);

            if (board is null)
            {
                throw new BoardNotFoundException();
            }

            var item = new Tasks
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                BoardId = boardId,
                StatusId = 1,
                AssigneeId = _currentUserServices.UserId
            };

            await _context.Task.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskTitleAsync(int boardId, int taskId, string title)
        {
            var task = await _context.Task.FindAsync(taskId);

            if (task is null) 
            {
                throw new TaskNotFoundException();
            }

            if(task.BoardId != boardId) 
            {
                throw new TaskHasDifferentBoardException();
            }

            if (task.AssigneeId != _currentUserServices.UserId) 
            { 
                throw new TaskHasDifferentAssigneeException();
            }

            task.Title = title;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskDescriptionAsync(int boardId, int taskId, string description)
        {
            var task = await _context.Task.FindAsync(taskId);

            if (task is null)
            {
                throw new TaskNotFoundException();
            }

            if (task.BoardId != boardId)
            {
                throw new TaskHasDifferentBoardException();
            }

            if (task.AssigneeId != _currentUserServices.UserId)
            {
                throw new TaskHasDifferentAssigneeException();
            }

            task.Description = description;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskStatusAsync(int boardId, int taskId, int statusId)
        {
            var task = await _context.Task.FindAsync(taskId);

            if (task is null)
            {
                throw new TaskNotFoundException();
            }

            if (task.BoardId != boardId)
            {
                throw new TaskHasDifferentBoardException();
            }

            if (task.AssigneeId != _currentUserServices.UserId)
            {
                throw new TaskHasDifferentAssigneeException();
            }

            if (((TasksStatus)task.StatusId == TasksStatus.InProgress && ((TasksStatus)statusId == TasksStatus.ToDo || (TasksStatus)statusId == TasksStatus.Done)) || (TasksStatus)task.StatusId == TasksStatus.ToDo && (TasksStatus)statusId == TasksStatus.InProgress)
            {
                task.StatusId = statusId;
            }
            else
            {
                throw new InvalidTaskStatusException();
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAssigneeAsync(int boardId, int taskId, int assigneeId)
        {
            var task = await _context.Task.FindAsync(taskId);

            if (task is null)
            {
                throw new TaskNotFoundException();
            }

            if (task.BoardId != boardId)
            {
                throw new TaskHasDifferentBoardException();
            }

            if (task.AssigneeId != _currentUserServices.UserId)
            {
                throw new TaskHasDifferentAssigneeException();
            }

            task.AssigneeId = assigneeId;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int boardId, int taskId)
        {
            var task = await _context.Task.FindAsync(taskId);

            if (task is null)
            {
                throw new TaskNotFoundException();
            }

            if (task.BoardId != boardId)
            {
                throw new TaskHasDifferentBoardException();
            }

            if (task.AssigneeId != _currentUserServices.UserId)
            {
                throw new TaskHasDifferentAssigneeException();
            }

            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
