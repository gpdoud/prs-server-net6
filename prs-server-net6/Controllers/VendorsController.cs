using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prs_server_net6.Models;

namespace prs_server_net6.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class VendorsController : ControllerBase {

        private readonly AppDbContext _context;
        public VendorsController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetAll() {
            return await _context.Vendors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> Get(int id) {
            var vendor = await _context.Vendors.FindAsync(id);
            return (vendor != null) ? vendor : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Vendor>> Create(Vendor vendor) {
            if(vendor == null) { return BadRequest(); }
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
            return Ok(vendor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Change(int id, Vendor vendor) {
            if(vendor.Id != id) { return BadRequest(); }
            _context.Entry(vendor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id) {
            var vendor = await _context.Vendors.FindAsync(id);
            if(vendor == null) { return NotFound(); }
            return NoContent();
        }
    }
}

