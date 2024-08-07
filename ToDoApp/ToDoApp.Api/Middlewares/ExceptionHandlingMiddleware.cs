using System.Net;
using System.Text.Json;
using ToDoApp.Services.Exceptions;

namespace ToDoApp.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch(ToDoAppBaseException toDoEx)
            {
                httpContext.Response.StatusCode = (int)toDoEx.StatusCode;
                var response = new { message = toDoEx.Message };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new { message = "Server Error" };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
