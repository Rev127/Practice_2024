using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces
{
    public interface IToDoTasksServices
    {
        Task<List<GetTaskDto>> GetTasksAsync(int boardId);
        Task CreateTaskAsync(int boardId, CreateTaskDto taskDto);
        Task UpdateTaskTitleAsync(int taskId, UpdateTaskDto updateTaskDto);
        Task UpdateTaskDescriptionAsync(int taskId, UpdateTaskDto updateTaskDto);
        Task UpdateTaskStatusAsync(int taskId, UpdateTaskDto updateTaskDto);
        Task UpdateAssigneeAsync(int taskId, UpdateTaskDto updateTaskDto);
        Task DeleteTaskAsync(int boardId, int taskId);
    }
}
