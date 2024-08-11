using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
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

        public async Task CreateTaskAsync(CreateTaskDto taskDto)
        {
            var board = await _context.Board.FindAsync(taskDto.BoardId);

            if (board is null)
            {
                throw new BoardNotFoundException();
            }

            var item = new Tasks
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                BoardId = taskDto.BoardId,
                StatusId = 1,
                AssigneeId = _currentUserServices.UserId
            };

            await _context.Task.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskTitleAsync(UpdateTaskDto updateTaskDto)
        {
            var task = await _context.Task.FindAsync(updateTaskDto.Id);

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

        public async Task UpdateTaskDescriptionAsync(UpdateTaskDto updateTaskDto)
        {
            var task = await _context.Task.FindAsync(updateTaskDto.Id);

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

        public async Task UpdateTaskStatusAsync(UpdateTaskDto updateTaskDto)
        {
            var task = await _context.Task.FindAsync(updateTaskDto.Id);

            if (task is null)
            {
                throw new TaskNotFoundException();
            }

            if (task.BoardId != updateTaskDto.BoardId)
            {
                throw new TaskHasDifferentBoardException();
            }

            var statusValidation = await _context.statusesValidations.Where(x => x.StatusId == task.StatusId && x.StatusValidationId == updateTaskDto.StatusId).FirstOrDefaultAsync();

            if (statusValidation is null)
            {
                throw new InvalidTaskStatusException();
            }
                
            task.StatusId = (int)updateTaskDto.StatusId;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAssigneeAsync(UpdateTaskDto updateTaskDto)
        {
            var task = await _context.Task.FindAsync(updateTaskDto.Id);

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

        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await _context.Task.FindAsync(taskId);

            if (task is null)
            {
                throw new TaskNotFoundException();
            }

            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
