using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Exceptions;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services
{
    public class ToDoBoardServices : IToDoBoardServices
    {
        private readonly ToDoContext _context;

        public ToDoBoardServices(ToDoContext context, ICurrentUserServices currentUserServices)
        {
            _context = context;
        }
        public async Task CreateBoardAsync(CreateBoardDto boardDto)
        {
            var item = new Boards
            {
                Name = boardDto.Name
            };

            await _context.Board.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<GetBoardDto> GetBoardAsync(int boardId)
        {
            var board = await _context.Board.FindAsync(boardId);

            if (board is null)
            {
                throw new BoardNotFoundException();
            }

            return await _context.Board
                .Where(x => x.Id == boardId)
                .Select(x => new GetBoardDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt
                })
                .FirstAsync();
        }

        public async Task<List<GetBoardDto>> GetAllBoardsAsync()
        {
            var boards = await _context.Board
                .Select(x => new GetBoardDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();

            if (!boards.Any())
            {
                throw new BoardNotFoundException();
            }

            return boards;
        }
    }
}
