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
    public class RequestsController : ControllerBase {

        private readonly AppDbContext _context;
        public RequestsController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetAll() {
            return await _context.Requests
                                    .Include(r => r.User)
                                    .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> Get(int id) {
            var request = await _context.Requests
                                            .Include(r => r.User)
                                            .Include(r => r.Requestlines)
                                                .ThenInclude(rl => rl.Product)
                                            .SingleOrDefaultAsync(r => r.Id == id);
            return (request != null) ? request : NotFound();
        }

        [HttpGet("reviews/{userId}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetReviews(int userId) {
            return await _context.Requests
                                    .Where(r => r.Status.Equals("REVIEW") && r.UserId != userId)
                                    .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Request>> Create(Request request) {
            if (request == null) { return BadRequest(); }
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            return Ok(request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Change(int id, Request request) {
            if (request.Id != id) { return BadRequest(); }
            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("review")]
        public async Task<IActionResult> SetReview(Request request) {
            var status = (request.Total <= 50) ? "APPROVED" : "REVIEW";
            var newRequest = request with { Status = status };
            return await Change(newRequest.Id, newRequest);
        }

        [HttpPut("approve")]
        public async Task<IActionResult> SetApproved(Request request) {
            var status = "APPROVED";
            var newRequest = request with { Status = status };
            return await Change(newRequest.Id, newRequest);
        }

        [HttpPut("reject")]
        public async Task<IActionResult> SetRejected(Request request) {
            var status = "REJECTED";
            var newRequest = request with { Status = status };
            return await Change(newRequest.Id, newRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id) {
            var request = await _context.Requests.FindAsync(id);
            if (request == null) { return NotFound(); }
            return NoContent();
        }
    }
}


