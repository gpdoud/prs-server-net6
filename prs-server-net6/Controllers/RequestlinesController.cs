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
    public class RequestlinesController : ControllerBase {

        private readonly AppDbContext _context;
        public RequestlinesController(AppDbContext context) { _context = context; }

        private async Task RecalculateRequestTotal(int requestId) {
            var request = await _context.Requests.FindAsync(requestId);
            if(request == null) { throw new Exception("Invalid requestId in RecalculateRequestTotal"); }
            var total = (from rl in _context.Requestlines
                             join p in _context.Products
                             on rl.ProductId equals p.Id
                             where rl.RequestId == requestId
                             select new {
                                 LineTotal = rl.Quantity * p.Price
                             }).Sum(x => x.LineTotal);
            var newRequest = request with { Total = total };
            _context.Entry(newRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requestline>>> GetAll() {
            return await _context.Requestlines.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Requestline>> Get(int id) {
            var product = await _context.Requestlines.FindAsync(id);
            return (product != null) ? product : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Requestline>> Create(Requestline requestline) {
            if (requestline == null) { return BadRequest(); }
            _context.Requestlines.Add(requestline);
            await _context.SaveChangesAsync();
            await RecalculateRequestTotal(requestline.RequestId);
            return Ok(requestline);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Change(int id, Requestline requestline) {
            if (requestline.Id != id) { return BadRequest(); }
            _context.Entry(requestline).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await RecalculateRequestTotal(requestline.RequestId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id) {
            var requestline = await _context.Requestlines.FindAsync(id);
            if (requestline == null) { return NotFound(); }
            _context.Requestlines.Remove(requestline);
            await _context.SaveChangesAsync();
            await RecalculateRequestTotal(requestline.RequestId);
            return NoContent();
        }
    }
}


