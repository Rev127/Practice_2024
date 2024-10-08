﻿namespace ToDoApp.Data.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int BoardId { get; set; }
        public Boards Board { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StatusId { get; set; }
        public Statuses Status { get; set; }
        public string? AssigneeId { get; set; }
        public Users User { get; set; }

    }
}
