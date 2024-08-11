using System.Net;

namespace ToDoApp.Services.Exceptions
{
    public class UserNotFoundException : ToDoAppBaseException
    {
        public UserNotFoundException() : base("User not found", HttpStatusCode.NotFound)
        {
        }
    }
}
