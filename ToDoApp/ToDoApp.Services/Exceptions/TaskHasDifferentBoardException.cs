using System.Net;

namespace ToDoApp.Services.Exceptions
{
    public class TaskHasDifferentBoardException : ToDoAppBaseException
    {
        public TaskHasDifferentBoardException() : base("Task has different board", HttpStatusCode.Forbidden)
        {
        }
    }
}
