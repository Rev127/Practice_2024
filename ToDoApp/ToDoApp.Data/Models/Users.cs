using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Data.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Tasks> Tasks { get; set; }
    }
}
