using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ipdms.Models;
using ipdms.Models.AppDbContext;

namespace ipdms.Controllers.LookUpController
{
    [Route("[controller]")]
    [ApiController]
    public class OfficeActionsController : ControllerBase
    {
        private readonly IpdmsDbContext _context;

        public OfficeActionsController(IpdmsDbContext context)
        {
            _context = context;
        }

        // GET: api/OfficeActions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfficeAction>>> GetOfficeAction()
        {
            return await _context.OfficeAction.ToListAsync();
        }

        // GET: api/OfficeActions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OfficeAction>> GetOfficeAction(int id)
        {
            var officeAction = await _context.OfficeAction.FindAsync(id);

            if (officeAction == null)
            {
                return NotFound();
            }

            return officeAction;
        }

        // PUT: api/OfficeActions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfficeAction(int id, OfficeAction officeAction)
        {
            if (id != officeAction.office_action_id)
            {
                return BadRequest();
            }

            _context.Entry(officeAction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficeActionExists(id))
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

        // POST: api/OfficeActions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OfficeAction>> PostOfficeAction(OfficeAction officeAction)
        {
            _context.OfficeAction.Add(officeAction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOfficeAction", new { id = officeAction.office_action_id }, officeAction);
        }

        // DELETE: api/OfficeActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfficeAction(int id)
        {
            var officeAction = await _context.OfficeAction.FindAsync(id);
            if (officeAction == null)
            {
                return NotFound();
            }

            _context.OfficeAction.Remove(officeAction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OfficeActionExists(int id)
        {
            return _context.OfficeAction.Any(e => e.office_action_id == id);
        }
    }
}
