using Microsoft.EntityFrameworkCore;
using ToDoApp.Api.Middlewares;
using ToDoApp.Data.Context;
using ToDoApp.Services.Interfaces;
using ToDoApp.Services.Services;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("ToDoAppDB");

builder.Services.AddDbContext<ToDoContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IToDoBoardServices, ToDoBoardServices>();
builder.Services.AddScoped<IToDoTasksServices, ToDoTasksServices>();
builder.Services.AddScoped<ICurrentUserServices, CurrentUserServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
