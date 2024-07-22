using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [Route("api/to-do-boards")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly IToDoBoardServices _services;

        public BoardsController(IToDoBoardServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetBoardsAsync()
        {
            var boards = await _services.GetBoardAsync();
            return Ok(boards);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBoardAsync(CreateBoardDto boardDto)
        {
            await _services.CreateBoardAsync(boardDto);
            return Ok();
        }
    }
}
