using System.Security.Claims;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services
{
    public class CurrentUserServices : ICurrentUserServices
    {
        private readonly HttpContext _context;

        public CurrentUserServices(IHttpContextAccessor httpContext)
        {
            _context = httpContext.HttpContext!;
        }

        public string UserId => _context!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}
