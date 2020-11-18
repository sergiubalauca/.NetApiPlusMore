using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthAPI.Context;
using HealthAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly SQLWorkshopContext _context;

        public AddressesController(SQLWorkshopContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public IEnumerable<Address> Get()
        {
            return _context.Addresses;
        }

        // GET: api/Addresses/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Addresses
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Addresses/5
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
