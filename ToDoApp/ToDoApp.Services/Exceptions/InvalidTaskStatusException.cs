using System.Net;

namespace ToDoApp.Services.Exceptions
{
    public class InvalidTaskStatusException : ToDoAppBaseException
    {
        public InvalidTaskStatusException() : base("Invalid task status", HttpStatusCode.Forbidden)
        {
        }
    }
}
