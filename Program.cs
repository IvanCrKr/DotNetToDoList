using Microsoft.EntityFrameworkCore;
using DotNetToDoList.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MSSQL server
builder.Services.AddDbContext<TodoContext>(options =>
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("TodoContext"))
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine)
);

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
