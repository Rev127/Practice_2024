using Microsoft.AspNetCore.Identity;

namespace ToDoApp.Data.Models
{
    public class Users : IdentityUser
    {
        public ICollection<Tasks> Tasks { get; set; }
    }
}
