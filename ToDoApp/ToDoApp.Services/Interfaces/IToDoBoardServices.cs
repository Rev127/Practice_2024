using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces
{
    public interface IToDoBoardServices
    {
        Task<List<GetBoardDto>> GetBoardsAsync();
        Task<GetBoardDto> GetBoardAsync(int boardId);
        Task CreateBoardAsync(CreateBoardDto boardDto);
    }
}
