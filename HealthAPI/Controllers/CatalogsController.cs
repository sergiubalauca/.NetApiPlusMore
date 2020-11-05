using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthAPI.Context;
using HealthAPI.Models;

namespace HealthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly SQLWorkshopContext _context;

        public CatalogsController(SQLWorkshopContext context)
        {
            _context = context;
        }

        // GET: api/Catalogs
        [HttpGet]
        public IEnumerable<Catalog> GetCatalog()
        {
            return _context.Catalog;
        }

        // GET: api/Catalogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var catalog = await _context.Catalog.FindAsync(id);

            if (catalog == null)
            {
                return NotFound();
            }

            return Ok(catalog);
        }

        // PUT: api/Catalogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalog([FromRoute] int id, [FromBody] Catalog catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != catalog.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(catalog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogExists(id))
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

        // POST: api/Catalogs
        [HttpPost]
        public async Task<IActionResult> PostCatalog([FromBody] Catalog catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Catalog.Add(catalog);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CatalogExists(catalog.CourseId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCatalog", new { id = catalog.CourseId }, catalog);
        }

        // DELETE: api/Catalogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var catalog = await _context.Catalog.FindAsync(id);
            if (catalog == null)
            {
                return NotFound();
            }

            _context.Catalog.Remove(catalog);
            await _context.SaveChangesAsync();

            return Ok(catalog);
        }

        private bool CatalogExists(int id)
        {
            return _context.Catalog.Any(e => e.CourseId == id);
        }
    }
}