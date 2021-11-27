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

        [HttpPut("review/{id}")]
        public async Task<IActionResult> SetReview(int id, Request request) {
            var req = await _context.Requests.FindAsync(request.Id);
            if (req == null) { return NotFound(); }
            req.Status = (req.Total <= 50) ? "APPROVED" : "REVIEW";
            req.RejectionReason = null;
            return await Change(req.Id, req);
        }

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> SetApproved(int id, Request request) {
            var req = await _context.Requests.FindAsync(request.Id);
            if (req == null) { return NotFound(); }
            req.Status = "APPROVED";
            req.RejectionReason = null;
            return await Change(req.Id, req);
        }

        [HttpPut("reject/{id}")]
        public async Task<IActionResult> SetRejected(int id, Request request) {
            var req = await _context.Requests.FindAsync(request.Id);
            if (req == null) { return NotFound(); }
            req.Status = "REJECTED";
            req.RejectionReason = request.RejectionReason;
            return await Change(req.Id, req);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id) {
            var request = await _context.Requests.FindAsync(id);
            if (request == null) { return NotFound(); }
            return NoContent();
        }
    }
}


