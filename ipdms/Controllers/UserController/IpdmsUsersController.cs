using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ipdms.Models;
using ipdms.Models.AppDbContext;

namespace ipdms.Controllers.UserController
{
    [Route("[controller]")]
    [ApiController]
    public class IpdmsUsersController : ControllerBase
    {
        private readonly IpdmsDbContext _context;

        public IpdmsUsersController(IpdmsDbContext context)
        {
            _context = context;
        }

        // GET: api/IpdmsUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IpdmsUser>>> GetIpdmsUser()
        {
            return await _context.IpdmsUser.ToListAsync();
        }

        // GET: api/IpdmsUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IpdmsUser>> GetIpdmsUser(int id)
        {
            var ipdmsUser = await _context.IpdmsUser.FindAsync(id);

            if (ipdmsUser == null)
            {
                return NotFound();
            }

            return ipdmsUser;
        }

        // PUT: api/IpdmsUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIpdmsUser(int id, IpdmsUser ipdmsUser)
        {
            if (id != ipdmsUser.user_id)
            {
                return BadRequest();
            }

            _context.Entry(ipdmsUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IpdmsUserExists(id))
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

        // POST: api/IpdmsUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IpdmsUser>> PostIpdmsUser(IpdmsUser ipdmsUser)
        {
            _context.IpdmsUser.Add(ipdmsUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIpdmsUser", new { id = ipdmsUser.user_id }, ipdmsUser);
        }

        // DELETE: api/IpdmsUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IpdmsUser>> DeleteIpdmsUser(int id)
        {
            var ipdmsUser = await _context.IpdmsUser.FindAsync(id);
            if (ipdmsUser == null)
            {
                return NotFound();
            }

            _context.IpdmsUser.Remove(ipdmsUser);
            await _context.SaveChangesAsync();

            return ipdmsUser;
        }

        private bool IpdmsUserExists(int id)
        {
            return _context.IpdmsUser.Any(e => e.user_id == id);
        }
    }
}
