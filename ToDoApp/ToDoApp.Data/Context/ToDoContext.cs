using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Models;


namespace ToDoApp.Data.Context
{
    public class ToDoContext : IdentityDbContext<Users>
    {
        public DbSet<Boards> Board { get; set; }
        public DbSet<Statuses> Statuss { get; set; }
        public DbSet<Tasks> Task {  get; set; }
        public DbSet<StatusesValidation> statusesValidations { get; set; }

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
                .HasMany(t => t.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.AssigneeId);

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

            modelBuilder.Entity<Statuses>()
                .HasMany(t => t.ValidationStatuses)
                .WithOne(t => t.ValidationStatus)
                .HasForeignKey(t => t.StatusId);

            modelBuilder.Entity<Statuses>()
                .HasMany(t => t.StatusesValidation)
                .WithOne(t => t.StatusValidation)
                .HasForeignKey(t => t.StatusValidationId)
                .OnDelete(DeleteBehavior.Restrict);

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

            modelBuilder.Entity<StatusesValidation>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<StatusesValidation>().HasData(
                new StatusesValidation
                {
                    Id = 1,
                    StatusId = 1,
                    StatusValidationId = 2,
                    StatusValidationName = "To Do -> In Progress"
                },

                new StatusesValidation
                {
                    Id = 2,
                    StatusId = 2,
                    StatusValidationId = 1,
                    StatusValidationName = "In Progress -> To Do"
                },

                new StatusesValidation
                {
                    Id = 3,
                    StatusId = 2,
                    StatusValidationId = 3,
                    StatusValidationName = "In Progress -> Done"
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
