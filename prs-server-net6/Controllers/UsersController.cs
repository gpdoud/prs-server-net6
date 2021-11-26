using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prs_server_net6.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace prs_server_net6.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase {

        private readonly AppDbContext _context;
        public UsersController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll() {
            return await _context.Users.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id) {
            var user = await _context.Users.FindAsync(id);
            if(user == null) {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<User>> Login(string username, string password) {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user == null) {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<User>> Create(User user) {
            if(user == null) {
                return BadRequest();
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Created("Created", user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Change(int id, User user) {
            if (user.Id != id) {
                return BadRequest();
            }
            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id) {
            var user = await _context.Users.FindAsync(id);
            if(user == null) {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

