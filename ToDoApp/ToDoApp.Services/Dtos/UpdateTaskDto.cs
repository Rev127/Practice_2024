using ToDoApp.Data.Models;

namespace ToDoApp.Services.Dtos
{
    public class UpdateTaskDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int BoardId { get; set; }
        public int? StatusId { get; set; }
        public int? AssigneeId { get; set; }
    }
}
