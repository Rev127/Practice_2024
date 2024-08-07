using Microsoft.AspNetCore.Mvc;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IToDoUsersServices _services;

        public UsersController(IToDoUsersServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllUsersAsync()
        {
            var users = await _services.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetUsersByNameAsync(string name)
        {
            var users = await _services.GetUsersByNameAsync(name);
            return Ok(users);
        }
    }
}
