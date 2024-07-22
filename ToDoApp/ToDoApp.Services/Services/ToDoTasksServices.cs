using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services
{
    public class ToDoTasksServices : IToDoTasksServices
    {
        private readonly ToDoContext _context;
        private readonly ICurrentUserServices _currentUserServices;
        private readonly ICurrentBoardServises _currentBoardServises;

        public ToDoTasksServices(ToDoContext context, ICurrentUserServices currentUserServices, ICurrentBoardServises currentBoardServises) 
        {
            _context = context;
            _currentUserServices = currentUserServices;
            _currentBoardServises = currentBoardServises;
        }


        public async Task<List<Tasks>> GetTasksAsync()
        {
            return await _context.Task.Where(x => x.AssigneeId == _currentUserServices.UserId && x.BoardId == _currentBoardServises.BoardId).ToListAsync();
        }

        public async Task CreateTaskAsync(CreateTaskDto taskDto)
        {
            var item = new Tasks
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                BoardId = _currentBoardServises.BoardId,
                StatusId = 1,
                AssigneeId = _currentUserServices.UserId
            };

            await _context.Task.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskTitleAsync(int id, string title)
        {
            var task = await _context.Task.FindAsync(id);

            if (task is null) { }

            if (task.AssigneeId != _currentUserServices.UserId) { }

            task.Title = title;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskDescriptionAsync(int id, string description)
        {
            var task = await _context.Task.FindAsync(id);

            if (task is null) { }

            if (task.AssigneeId != _currentUserServices.UserId) { }

            task.Description = description;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskStatusAsync(int id, int statusId)
        {
            var task = await _context.Task.FindAsync(id);

            if (task is null) { }

            if (task.AssigneeId != _currentUserServices.UserId) { }

            if ((task.StatusId == 2 && (statusId == 1 || statusId == 3)) || task.StatusId == 1 && statusId == 2)
            {
                task.StatusId = statusId;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAssigneeAsync(int id, int assigneeId)
        {
            var task = await _context.Task.FindAsync(id);

            if (task is null) { }

            if (task.AssigneeId != _currentUserServices.UserId) { }

            task.AssigneeId = assigneeId;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Task.FindAsync(id);

            if (task is null) { }

            if (task.AssigneeId != _currentUserServices.UserId) { }

            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
