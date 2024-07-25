using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces
{
    public interface IToDoTasksServices
    {
        Task<List<Tasks>> GetTasksAsync(int boardId);
        Task CreateTaskAsync(int boardId, CreateTaskDto taskDto);
        Task UpdateTaskTitleAsync(int boardId, int taskId, string title);
        Task UpdateTaskDescriptionAsync(int boardId, int taskId, string description);
        Task UpdateTaskStatusAsync(int boardId, int taskId, int statusId);
        Task UpdateAssigneeAsync(int boardId, int taskId, int assigneeId);
        Task DeleteTaskAsync(int boardId, int taskId);
    }
}
