using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthAPI.Context;
using HealthAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDo : ControllerBase
    {
        private readonly SQLWorkshopContext _context;

        public ToDo(SQLWorkshopContext context)
        {
            _context = context;
        }

        // GET: api/ToDos
        [HttpGet]
        public IEnumerable<ToDos> Get()
        {
            return _context.ToDos;
        }

        // GET: api/ToDos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDos([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var students = await _context.Students.FindAsync(id);
            var todo = await _context.ToDos.FirstOrDefaultAsync(i => i.ToDoID == Int32.Parse(id));
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // POST: api/ToDos
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ToDos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
