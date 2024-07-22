using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services
{
    public class ToDoBoardServices : IToDoBoardServices
    {
        private readonly ToDoContext _context;
        private readonly ICurrentBoardServises _currentBoardServises;

        public ToDoBoardServices(ToDoContext context, ICurrentUserServices currentUserServices, ICurrentBoardServises currentBoardServises)
        {
            _context = context;
            _currentBoardServises = currentBoardServises;
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

        public async Task<List<Boards>> GetBoardAsync()
        {
            return await _context.Board.Where(x => x.Id == _currentBoardServises.BoardId).ToListAsync();
        }
    }
}
