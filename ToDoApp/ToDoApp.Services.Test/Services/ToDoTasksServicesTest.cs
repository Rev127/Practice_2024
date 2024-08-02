using Microsoft.EntityFrameworkCore;
using Moq;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Enums;
using ToDoApp.Services.Exceptions;
using ToDoApp.Services.Interfaces;
using ToDoApp.Services.Services;

namespace ToDoApp.Services.Test.Services
{
    public class ToDoTasksServicesTest
    {
        private readonly int UserId = 1;

        [Fact]
        public async Task UpdateTaskTitleAsync_WithNonExistingItem_TaskNotFoundException()
        {
            // Arrange
            var currentUserServices = new Mock<ICurrentUserServices>();
            currentUserServices.Setup(x => x.UserId).Returns(UserId);

            var service = new ToDoTasksServices(GetToDoContext(), currentUserServices.Object);

            var updateTaskDto = new UpdateTaskDto
            {
                BoardId = 1,
            };

            // Act
            Func<Task> act = async () => await service.UpdateTaskTitleAsync(1, updateTaskDto);

            // Assert
            await Assert.ThrowsAsync<TaskNotFoundException>(act);
        }



        [Fact]
        public async Task UpdateTaskTitleAsync_TaskHasDifferentBoard_TaskHasDifferentBoardException()
        {
            // Arrange
            var currentUserServices = new Mock<ICurrentUserServices>();
            currentUserServices.Setup(x => x.UserId).Returns(UserId);

            var context = GetToDoContext();

            var task = new Tasks
            {
                Id = 1,
                Title = "Test",
                BoardId = 2
            };

            await context.Task.AddAsync(task);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new ToDoTasksServices(context, currentUserServices.Object);

            var updateTaskDto = new UpdateTaskDto
            {
                BoardId = 1,
                StatusId = 1
            };

            // Act
            Func<Task> act = async () => await service.UpdateTaskTitleAsync(1, updateTaskDto);

            // Assert
            await Assert.ThrowsAsync<TaskHasDifferentBoardException>(act);
        }


        [Theory]
        [InlineData("Test", "Update Test")]
        public async Task UpdateTaskTitleAsync_TaskTitleСhanged(string currentTitle, string expectedTitle)
        {
            // Arrange
            var currentUserServices = new Mock<ICurrentUserServices>();
            currentUserServices.Setup(x => x.UserId).Returns(UserId);

            var context = GetToDoContext();

            var task = new Tasks
            {
                Id = 1,
                Title = currentTitle,
                BoardId = 1
            };

            await context.Task.AddAsync(task);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new ToDoTasksServices(context, currentUserServices.Object);

            var updateTaskDto = new UpdateTaskDto
            {
                Title = expectedTitle,
                BoardId = 1
            };

            // Act
            await service.UpdateTaskTitleAsync(1, updateTaskDto);

            // Assert
            var updateTask = await context.Task.FindAsync(1);
            Assert.Equal(expectedTitle, updateTask.Title);
        }

        [Fact]
        public async Task UpdateTaskDescriptionAsync_WithNonExistingItem_TaskNotFoundException()
        {
            // Arrange
            var currentUserServices = new Mock<ICurrentUserServices>();
            currentUserServices.Setup(x => x.UserId).Returns(UserId);

            var service = new ToDoTasksServices(GetToDoContext(), currentUserServices.Object);

            var updateTaskDto = new UpdateTaskDto
            {
                BoardId = 1,
            };

            // Act
            Func<Task> act = async () => await service.UpdateTaskDescriptionAsync(1, updateTaskDto);

            // Assert
            await Assert.ThrowsAsync<TaskNotFoundException>(act);
        }



        [Fact]
        public async Task UpdateTaskDescriptionAsync_TaskHasDifferentBoard_TaskHasDifferentBoardException()
        {
            // Arrange
            var currentUserServices = new Mock<ICurrentUserServices>();
            currentUserServices.Setup(x => x.UserId).Returns(UserId);

            var context = GetToDoContext();

            var task = new Tasks
            {
                Id = 1,
                Title = "Test",
                BoardId = 2
            };

            await context.Task.AddAsync(task);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new ToDoTasksServices(context, currentUserServices.Object);

            var updateTaskDto = new UpdateTaskDto
            {
                BoardId = 1,
            };

            // Act
            Func<Task> act = async () => await service.UpdateTaskDescriptionAsync(1, updateTaskDto);

            // Assert
            await Assert.ThrowsAsync<TaskHasDifferentBoardException>(act);
        }


        [Theory]
        [InlineData("Description", "Update description")]
        public async Task UpdateTaskDescriptionAsync_TaskDescriptionСhanged(string currentDescription, string expectedDescription)
        {
            // Arrange
            var currentUserServices = new Mock<ICurrentUserServices>();
            currentUserServices.Setup(x => x.UserId).Returns(UserId);

            var context = GetToDoContext();

            var task = new Tasks
            {
                Id = 1,
                Title = "Test",
                Description = currentDescription,
                BoardId = 1
            };

            await context.Task.AddAsync(task);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new ToDoTasksServices(context, currentUserServices.Object);

            var updateTaskDto = new UpdateTaskDto
            {
                Description = expectedDescription,
                BoardId = 1
            };

            // Act
            await service.UpdateTaskDescriptionAsync(1, updateTaskDto);

            // Assert
            var updateTask = await context.Task.FindAsync(1);
            Assert.Equal(expectedDescription, updateTask.Description);
        }


        [Fact]
        public async Task UpdateTaskStatusAsync_WithNonExistingItem_TaskNotFoundException()
        {
            // Arrange
            var currentUserServices = new Mock<ICurrentUserServices>();
            currentUserServices.Setup(x => x.UserId).Returns(UserId);

            var service = new ToDoTasksServices(GetToDoContext() ,currentUserServices.Object);

            var updateTaskDto = new UpdateTaskDto
            {
                BoardId = 1,
                StatusId = 1
            };

            // Act
            Func<Task> act = async () => await service.UpdateTaskStatusAsync(1, updateTaskDto);

            // Assert
            await Assert.ThrowsAsync<TaskNotFoundException>(act);
        }



        [Fact]
        public async Task UpdateTaskStatusAsync_TaskHasDifferentBoard_TaskHasDifferentBoardException()
        {
            // Arrange
            var currentUserServices = new Mock<ICurrentUserServices>();
            currentUserServices.Setup(x => x.UserId).Returns(UserId);

            var context = GetToDoContext();

            var task = new Tasks
            {
                Id = 1,
                Title = "Test",
                BoardId = 2
            };

            await context.Task.AddAsync(task);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new ToDoTasksServices(context, currentUserServices.Object);

            var updateTaskDto = new UpdateTaskDto
            {
                BoardId = 1,
                StatusId = 1
            };

            // Act
            Func<Task> act = async () => await service.UpdateTaskStatusAsync(1, updateTaskDto);

            // Assert
            await Assert.ThrowsAsync<TaskHasDifferentBoardException>(act);
        }


        [Theory]
        [InlineData(TasksStatus.ToDo, TasksStatus.InProgress)]
        [InlineData(TasksStatus.InProgress, TasksStatus.ToDo)]
        [InlineData(TasksStatus.InProgress, TasksStatus.Done)]
        public async Task UpdateTaskStatusAsync_TaskStatusСhanged(TasksStatus currentStatus, TasksStatus expectedStatus)
        {
            // Arrange
            var currentUserServices = new Mock<ICurrentUserServices>();
            currentUserServices.Setup(x => x.UserId).Returns(UserId);

            var context = GetToDoContext();

            var task = new Tasks
            {
                Id = 1,
                Title = "Test",
                BoardId = 1,
                StatusId = (int)currentStatus,
            };

            await context.Task.AddAsync(task);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new ToDoTasksServices(context, currentUserServices.Object);

            var updateTaskDto = new UpdateTaskDto
            {
                BoardId = 1,
                StatusId = (int)expectedStatus
            };

            // Act
            await service.UpdateTaskStatusAsync(1, updateTaskDto);

            // Assert
            var updateTask = await context.Task.FindAsync(1);
            Assert.Equal((int)expectedStatus, updateTask.StatusId);
        }

        [Fact]
        public async Task UpdateAssigneeAsync_WithNonExistingItem_TaskNotFoundException()
        {
            // Arrange
            var currentUserServices = new Mock<ICurrentUserServices>();
            currentUserServices.Setup(x => x.UserId).Returns(UserId);

            var service = new ToDoTasksServices(GetToDoContext(), currentUserServices.Object);

            var updateTaskDto = new UpdateTaskDto
            {
                BoardId = 1,
            };

            // Act
            Func<Task> act = async () => await service.UpdateAssigneeAsync(1, updateTaskDto);

            // Assert
            await Assert.ThrowsAsync<TaskNotFoundException>(act);
        }



        [Fact]
        public async Task UpdateAssigneeAsync_TaskHasDifferentBoard_TaskHasDifferentBoardException()
        {
            // Arrange
            var currentUserServices = new Mock<ICurrentUserServices>();
            currentUserServices.Setup(x => x.UserId).Returns(UserId);

            var context = GetToDoContext();

            var task = new Tasks
            {
                Id = 1,
                Title = "Test",
                BoardId = 2
            };

            await context.Task.AddAsync(task);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new ToDoTasksServices(context, currentUserServices.Object);

            var updateTaskDto = new UpdateTaskDto
            {
                BoardId = 1,
            };

            // Act
            Func<Task> act = async () => await service.UpdateAssigneeAsync(1, updateTaskDto);

            // Assert
            await Assert.ThrowsAsync<TaskHasDifferentBoardException>(act);
        }


        [Theory]
        [InlineData(1, 2)]
        public async Task UpdateAssigneeAsync_TaskAssigneeСhanged(int currentAssignee, int expectedAssignee)
        {
            // Arrange
            var currentUserServices = new Mock<ICurrentUserServices>();
            currentUserServices.Setup(x => x.UserId).Returns(UserId);

            var context = GetToDoContext();

            var task = new Tasks
            {
                Id = 1,
                Title = "Test",
                AssigneeId = currentAssignee,
                BoardId = 1
            };

            await context.Task.AddAsync(task);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new ToDoTasksServices(context, currentUserServices.Object);

            var updateTaskDto = new UpdateTaskDto
            {
                AssigneeId = expectedAssignee,
                BoardId = 1
            };

            // Act
            await service.UpdateAssigneeAsync(1, updateTaskDto);

            // Assert
            var updateTask = await context.Task.FindAsync(1);
            Assert.Equal(expectedAssignee, updateTask.AssigneeId);
        }

        private ToDoContext GetToDoContext()
        {
            var options = new DbContextOptionsBuilder<ToDoContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ToDoContext(options);

            context.Statuss.AddRange(
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

            context.statusesValidations.AddRange(
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

            context.SaveChanges();
            return context;
        }

    }
}
