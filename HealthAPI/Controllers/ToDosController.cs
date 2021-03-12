using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthAPI.Context;
using HealthAPI.Models;
using HealthAPI.Filters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly SQLWorkshopContext _context;

        public ToDosController(SQLWorkshopContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        //[Authorize("test")]
        public IEnumerable<ToDos> GetToDos()
        {
            var test = _context.ToDos.Where(todo => todo.ToDoID == 2).FirstOrDefault();
            if (test == null)
            {
                Console.WriteLine("empty to do");
            }
            return _context.ToDos;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var students = await _context.Students.FindAsync(id);
            var todos = await _context.ToDos.FirstOrDefaultAsync(i => i.ToDoID == id);
            if (todos == null)
            {
                return NotFound();
            }

            return Ok(todos);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDos([FromRoute] int id, [FromBody] ToDos todos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todos.ToDoID)
            {
                return BadRequest();
            }

            _context.Entry(todos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> PostToDos([FromBody] ToDos todos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ToDos.Add(todos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDos", new { id = todos.ToDoID }, todos);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todos = await _context.ToDos.FindAsync(id);
            if (todos == null)
            {
                return NotFound();
            }

            _context.ToDos.Remove(todos);
            await _context.SaveChangesAsync();

            return Ok(todos);
        }

        private bool ToDosExists(int id)
        {
            return _context.ToDos.Any(e => e.ToDoID == id);
        }
    }
}
