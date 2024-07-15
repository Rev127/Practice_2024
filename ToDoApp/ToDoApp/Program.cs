using ToDoApp.Data.Models;
using ToDoApp.Data.Context;


namespace ToDoApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var context = new ToDoContext();

            var user1 = new Users { Name = "Taras" };
            var user2 = new Users { Name = "Ivan" };

            var board1 = new Boards { Name = "Project 1" };
            var board2 = new Boards { Name = "Project 2" };

            var task1 = new Tasks
            {
                Title = "Task 1",
                Description = "Description for Task 1",
                Board = board1,
                StatusId = 1,
                User = user1
            };

            var task2 = new Tasks
            {
                Title = "Task 2",
                Description = "Description for Task 2",
                Board = board1,
                StatusId = 2,
                User = user2
            };

            var task3 = new Tasks
            {
                Title = "Task 3",
                Description = "Description for Task 3",
                Board = board2,
                StatusId = 3,
                User = user1
            };

            context.User.Add(user1);
            context.User.Add(user2);

            context.Board.Add(board1);
            context.Board.Add(board2);
            
            context.Task.Add(task1);
            context.Task.Add(task2);
            context.Task.Add(task3);

            await context.SaveChangesAsync();
        }
    }
}
