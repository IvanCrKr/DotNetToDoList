using Microsoft.EntityFrameworkCore;
using DotNetToDoList.Models;

namespace DotNetToDoList.Data
{
    public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
    {
        public DbSet<TodoItem> TodoItems { get; set; }
    }   
}