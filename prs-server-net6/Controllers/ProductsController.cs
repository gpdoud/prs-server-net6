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
    public class ProductsController : ControllerBase {

        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll() {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id) {
            var product = await _context.Products.FindAsync(id);
            return (product != null) ? product : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product) {
            if (product == null) { return BadRequest(); }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Change(int id, Product product) {
            if (product.Id != id) { return BadRequest(); }
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id) {
            var product = await _context.Products.FindAsync(id);
            if (product == null) { return NotFound(); }
            return NoContent();
        }
    }
}


