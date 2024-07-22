using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces
{
    public interface IToDoBoardServices
    {
        Task<List<Boards>> GetBoardAsync();
        Task CreateBoardAsync(CreateBoardDto boardDto);
    }
}
