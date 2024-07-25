using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Services.Exceptions
{
    public class TaskHasDifferentAssigneeException : ToDoAppBaseException
    {
        public TaskHasDifferentAssigneeException() : base("Task has differernt assignee", HttpStatusCode.Forbidden)
        {
        }
    }
}
