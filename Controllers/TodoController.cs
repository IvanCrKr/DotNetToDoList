using Microsoft.AspNetCore.Mvc;
using DotNetToDoList.Data;
using DotNetToDoList.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController(TodoContext context) : ControllerBase
    {
        private readonly TodoContext _context = context;

        // POST /api/todo
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoItem todo)
        {
            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodos), new { id = todo.Id }, todo);
        }

        // GET /api/todo
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var todos = await _context.TodoItems.ToListAsync();
            return Ok(todos);
        }

        // PUT /api/todo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] TodoItem updatedTodo)
        {
            var existing = await _context.TodoItems.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Title = updatedTodo.Title;
            existing.IsCompleted = updatedTodo.IsCompleted;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/todo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
