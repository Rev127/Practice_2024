using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [Authorize]
    [Route("api/boards")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly IToDoBoardServices _services;

        public BoardsController(IToDoBoardServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBoardDto>>> GetBoardsAsync()
        {
            var boards = await _services.GetBoardsAsync();
            return Ok(boards);
        }

        [HttpGet("{boardId}")]
        public async Task<ActionResult<IEnumerable<GetBoardDto>>> GetBoardAsync(int boardId)
        {
            var board = await _services.GetBoardAsync(boardId);
            return Ok(board);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBoardAsync(CreateBoardDto boardDto)
        {
            await _services.CreateBoardAsync(boardDto);
            return Ok();
        }
    }
}
