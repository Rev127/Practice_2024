using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Services.Dtos
{
    public class GetTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int BoardId { get; set; }
        public int StatusId { get; set; }
        public int? AssigneeId { get; set; }
    }
}
