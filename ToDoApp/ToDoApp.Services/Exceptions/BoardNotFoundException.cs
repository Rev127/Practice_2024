using System.Net;

namespace ToDoApp.Services.Exceptions
{
    public class BoardNotFoundException : ToDoAppBaseException
    {
        public BoardNotFoundException() : base("Board not found", HttpStatusCode.NotFound)
        {
        }
    }
}
