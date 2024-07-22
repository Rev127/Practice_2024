using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Services.Interfaces;
using ToDoApp.Services.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
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
builder.Services.AddScoped<ICurrentBoardServises, CurrentBoardServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
