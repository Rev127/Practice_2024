using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Exceptions;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services
{
    public class ToDoUsersServices : IToDoUsersServices
    {
        private readonly ToDoContext _context;

        public ToDoUsersServices(ToDoContext context)
        {
            _context = context;
        }

        public async Task<List<GetUserDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(x => new GetUserDto
                {
                    Id = x.Id,
                    Name = x.UserName
                })
                .ToListAsync();
        }

        public async Task<List<GetUserDto>> GetUsersByNameAsync(string name)
        {
            var users = await _context.Users
                .Where(x => x.UserName.Contains(name))
                .Select(x => new GetUserDto
                {
                    Id = x.Id,
                    Name = x.UserName
                })
                .ToListAsync();

            if (!users.Any())
            {
                throw new UserNotFoundException();
            }

            return users;
        }
    }
}
