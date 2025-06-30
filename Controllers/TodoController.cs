using Microsoft.AspNetCore.Mvc;
using DotNetToDoList.Data;
using DotNetToDoList.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // ✅ Test DB connection
        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            Console.WriteLine("Trying...");
            try
            {
                var conn = _context.Database.GetDbConnection();
                conn.Open(); // 💥 will throw if something is wrong
                return Ok(new { connected = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    connected = false,
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }
    }
}
