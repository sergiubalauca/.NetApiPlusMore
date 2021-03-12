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
using System.Data;

namespace HealthAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly SQLWorkshopContext _context;

        public ImagesController(SQLWorkshopContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        //[Authorize("test")]
        public IEnumerable<Image> GetImage()
        {
            var test = _context.Images.Where(image => image.ImageID < 0).FirstOrDefault();
            if (test == null)
            {
                Console.WriteLine("empty images");
            }
            return _context.Images;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var students = await _context.Students.FindAsync(id);
            var image = await _context.Images.FirstOrDefaultAsync(i => i.ImageID == id);
            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }

        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> PostImages([FromBody] Image images)
        {
            DataTable resultQuery = new DataTable();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection("Data Source=.;Initial Catalog=Workshop2;Integrated Security=True;"))
            {
                connection.Open();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

          
                sb.Append("insert into  Workshop2.dbo.Images(ImageEncript) values (CAST('wahid' AS VARBINARY(MAX)))");
                String sql = sb.ToString();

                using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(sql, connection))
                {
                    using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                    {
                        resultQuery.Load(reader);
                    }
                }
            }

            /*_context.Images.Add(images);
            await _context.SaveChangesAsync();*/

            return CreatedAtAction("Images", new { id = "test" }, images);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImages([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var images = await _context.Images.FindAsync(id);
            if (images == null)
            {
                return NotFound();
            }

            _context.Images.Remove(images);
            await _context.SaveChangesAsync();

            return Ok(images);
        }
    }
}
