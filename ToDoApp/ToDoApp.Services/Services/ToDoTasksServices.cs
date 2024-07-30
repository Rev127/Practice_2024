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


        public async Task<List<GetTaskDto>> GetTasksAsync(int boardId)
        {
            var board = await _context.Board.FindAsync(boardId);

            if (board is null)
            {
                throw new BoardNotFoundException();
            }

            return await _context.Task
                .Where(x => x.AssigneeId == _currentUserServices.UserId && x.BoardId == boardId)
                .Select(x => new GetTaskDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    BoardId = x.BoardId,
                    StatusId = x.StatusId,
                    AssigneeId = x.AssigneeId
                })
                .ToListAsync();
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

        public async Task UpdateTaskTitleAsync(int taskId, UpdateTaskDto updateTaskDto)
        {
            var task = await _context.Task.FindAsync(taskId);

            if (task is null) 
            {
                throw new TaskNotFoundException();
            }

            if (task.BoardId != updateTaskDto.BoardId)
            {
                throw new TaskHasDifferentBoardException();
            }

            task.Title = updateTaskDto.Title;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskDescriptionAsync(int taskId, UpdateTaskDto updateTaskDto)
        {
            var task = await _context.Task.FindAsync(taskId);

            if (task is null)
            {
                throw new TaskNotFoundException();
            }

            if (task.BoardId != updateTaskDto.BoardId)
            {
                throw new TaskHasDifferentBoardException();
            }

            task.Description = updateTaskDto.Description;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskStatusAsync(int taskId, UpdateTaskDto updateTaskDto)
        {
            var task = await _context.Task.FindAsync(taskId);

            if (task is null)
            {
                throw new TaskNotFoundException();
            }

            if (task.BoardId != updateTaskDto.BoardId)
            {
                throw new TaskHasDifferentBoardException();
            }

            var statusValidation = await _context.statusesValidations.Where(x => x.StatusValidationId == updateTaskDto.StatusId).FirstAsync();

            if (task.StatusId == statusValidation.StatusId)
            {
                task.StatusId = (int)updateTaskDto.StatusId;
            }
            else
            {
                throw new InvalidTaskStatusException();
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAssigneeAsync(int taskId, UpdateTaskDto updateTaskDto)
        {
            var task = await _context.Task.FindAsync(taskId);

            if (task is null)
            {
                throw new TaskNotFoundException();
            }

            if (task.BoardId != updateTaskDto.BoardId)
            {
                throw new TaskHasDifferentBoardException();
            }

            task.AssigneeId = updateTaskDto.AssigneeId;
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

            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
