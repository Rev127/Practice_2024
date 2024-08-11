namespace ToDoApp.Data.Models
{
    public class StatusesValidation
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public Statuses ValidationStatus  { get; set; }
        public int StatusValidationId { get; set; }
        public Statuses StatusValidation { get; set; }
        public string StatusValidationName { get; set; }
    }
}
