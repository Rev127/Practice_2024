namespace ToDoApp.Data.Models
{
    public class Statuses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<StatusesValidation> ValidationStatuses  { get; internal set; }
        public ICollection<StatusesValidation> StatusesValidation { get; internal set; }
        
    }
}
