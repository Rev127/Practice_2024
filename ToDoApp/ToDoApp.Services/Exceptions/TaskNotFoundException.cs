using System.Net;

namespace ToDoApp.Services.Exceptions
{
    public class TaskNotFoundException : ToDoAppBaseException
    {
        public TaskNotFoundException() : base("Task not found", HttpStatusCode.NotFound)
        {
        }
    }
}
