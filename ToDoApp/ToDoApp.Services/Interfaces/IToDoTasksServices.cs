using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces
{
    public interface IToDoTasksServices
    {
        Task<List<Tasks>> GetTasksAsync();
        Task CreateTaskAsync(CreateTaskDto taskDto);
        Task UpdateTaskTitleAsync(int id, string title);
        Task UpdateTaskDescriptionAsync(int id, string description);
        Task UpdateTaskStatusAsync(int id, int statusId);
        Task UpdateAssigneeAsync(int id, int assigneeId);
        Task DeleteTaskAsync(int id);
    }
}
