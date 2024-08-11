using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces
{
    public interface IToDoUsersServices
    {
        Task<List<GetUserDto>> GetAllUsersAsync();
        Task<List<GetUserDto>> GetUsersByNameAsync(string name);
    }
}
