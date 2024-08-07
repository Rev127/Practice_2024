
namespace ToDoApp.Services.Dtos
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int BoardId { get; set; }
        public int? StatusId { get; set; }
        public string? AssigneeId { get; set; }
    }
}
