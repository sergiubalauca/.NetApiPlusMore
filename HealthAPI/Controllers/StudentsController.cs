using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthAPI.Context;
using HealthAPI.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;

namespace HealthAPI.Controllers
{
    /* enable CORS (3)*/
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [Route("api/[controller]/1")] /*will this work for deleting a student?*/
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SQLWorkshopContext _context;

        public StudentsController(SQLWorkshopContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public IEnumerable<Students> GetStudents()
        {
            return _context.Students;
        }

        [HttpGet("sql")]
        public string GetStudentsSqlQuery()
        {
            string sqlResult = "";
            DataTable resultQuery = new DataTable();

            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder
            {
                DataSource = ".",
                InitialCatalog = "Workshop2"
            };

            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection("Data Source=.;Initial Catalog=Workshop2;Integrated Security=True;"))
            {
                connection.Open();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                /*sb.Append("SELECT pc.Name as StudentName cat.Grade as Grade");
                sb.Append("FROM [Students] pc ");
                sb.Append("JOIN [Catalog] cat");
                sb.Append("ON pc.ID = cat.StudentId;");*/

                sb.Append("select Cat.Grade, S.Name from Catalog Cat left join Students S on Cat.StudentID = S.ID");
                /*sb.Append("FROM [Catalog] Cat ");
                sb.Append("right JOIN [Students] S");
                sb.Append("ON Cat.StudentId = S.Id;");*/

                String sql = sb.ToString();
                
                using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(sql, connection))
                {
                    using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                    {
                        //while (reader.Read())
                       // {
                            //sqlResult = sqlResult + reader.GetInt32(0);

                        //}
                        resultQuery.Load(reader);
                    }
                }
            }
            foreach (DataRow row in resultQuery.Rows)
            {
                foreach (DataColumn col in resultQuery.Columns)
                {
                    sqlResult += row[col];
                }
            }

            return sqlResult;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudents([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var students = await _context.Students.FindAsync(id);
            var students = await _context.Students.FirstOrDefaultAsync(i => i.ID == Int32.Parse(id));
            if (students == null)
            {
                return NotFound();
            }

            return Ok(students);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudents([FromRoute] string id, [FromBody] Students students)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Int32.Parse(id) != students.ID)
            {
                return BadRequest();
            }

            _context.Entry(students).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsExists(id))
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
        public async Task<IActionResult> PostStudents([FromBody] Students students)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Students.Add(students);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudents", new { id = students.Name }, students);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudents([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var students = await _context.Students.FindAsync(Int32.Parse(id));
            if (students == null)
            {
                return NotFound();
            }

            _context.Students.Remove(students);
            await _context.SaveChangesAsync();

            return Ok(students);
        }

        private bool StudentsExists(string id)
        {
            return _context.Students.Any(e => e.Name == id);
        }
    }
}