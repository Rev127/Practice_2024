using System.Net;

namespace ToDoApp.Services.Exceptions
{
    public class ToDoAppBaseException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public ToDoAppBaseException(string message, HttpStatusCode statusCode) : base(message) 
        { 
            this.StatusCode = statusCode;
        }
    }
}
