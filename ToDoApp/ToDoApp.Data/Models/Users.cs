namespace ToDoApp.Data.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        //public ICollection<Boards> Boards { get; set; }
    }
}
