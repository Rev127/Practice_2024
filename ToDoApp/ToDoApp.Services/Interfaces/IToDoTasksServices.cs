using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces
{
    public interface IToDoTasksServices
    {
        Task<List<GetTaskDto>> GetTasksAsync(int boardId);
        Task CreateTaskAsync(CreateTaskDto taskDto);
        Task UpdateTaskTitleAsync(UpdateTaskDto updateTaskDto);
        Task UpdateTaskDescriptionAsync(UpdateTaskDto updateTaskDto);
        Task UpdateTaskStatusAsync(UpdateTaskDto updateTaskDto);
        Task UpdateAssigneeAsync(UpdateTaskDto updateTaskDto);
        Task DeleteTaskAsync(int taskId);
    }
}
