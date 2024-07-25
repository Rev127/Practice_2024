using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Models;


namespace ToDoApp.Data.Context
{
    public class ToDoContext : DbContext
    {
        public DbSet<Boards> Board { get; set; }
        public DbSet<Users> User { get; set; }
        public DbSet<Statuses> Statuss { get; set; }
        public DbSet<Tasks> Task {  get; set; }

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boards>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Boards>()
                .Property(t => t.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<Boards>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Boards>()
                .HasMany(t => t.Tasks)
                .WithOne(t => t.Board)
                .HasForeignKey(t => t.BoardId);

            modelBuilder.Entity<Users>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Users>()
                .Property(t => t.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<Users>()
                .HasMany(t => t.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.AssigneeId);

            //modelBuilder.Entity<Users>()
            //   .HasMany(t => t.Boards)
            //   .WithOne(t => t.User)
            //   .HasForeignKey(t => t.CreatedById)
            //   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tasks>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Tasks>()
                .Property(t => t.Title)
                .HasMaxLength(50);

            modelBuilder.Entity<Tasks>()
                .Property(t => t.Description)
                .HasMaxLength(150);

            modelBuilder.Entity<Tasks>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Statuses>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Statuses>()
                .Property(t => t.Name)
                .HasMaxLength(20);

            modelBuilder.Entity<Statuses>()
                .HasMany(t => t.Tasks)
                .WithOne(t => t.Status)
                .HasForeignKey(t => t.StatusId);

            modelBuilder.Entity<Statuses>().HasData(
                new Statuses
                {
                    Id = 1,
                    Name = "To Do"
                },

                new Statuses
                {
                    Id = 2,
                    Name = "In Progress"
                },

                new Statuses
                {
                    Id = 3,
                    Name = "Done"
                }
            );

            modelBuilder.Entity<Boards>().HasData(new Boards { Id = 1 });
            modelBuilder.Entity<Users>().HasData(new Users { Id = 1 });
            modelBuilder.Entity<Tasks>().HasData(new Tasks { Id = 1 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
