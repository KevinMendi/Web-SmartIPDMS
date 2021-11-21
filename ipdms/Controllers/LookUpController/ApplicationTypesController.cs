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
    public class ApplicationTypesController : ControllerBase
    {
        private readonly IpdmsDbContext _context;

        public ApplicationTypesController(IpdmsDbContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationType>>> GetApplicationType()
        {
            return await _context.ApplicationType.ToListAsync();
        }

        // GET: api/ApplicationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationType>> GetApplicationType(int id)
        {
            var applicationType = await _context.ApplicationType.FindAsync(id);

            if (applicationType == null)
            {
                return NotFound();
            }

            return applicationType;
        }

        // PUT: api/ApplicationTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationType(int id, ApplicationType applicationType)
        {
            if (id != applicationType.application_type_id)
            {
                return BadRequest();
            }

            _context.Entry(applicationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationTypeExists(id))
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

        // POST: api/ApplicationTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationType>> PostApplicationType(ApplicationType applicationType)
        {
            _context.ApplicationType.Add(applicationType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationType", new { id = applicationType.application_type_id }, applicationType);
        }

        // DELETE: api/ApplicationTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationType(int id)
        {
            var applicationType = await _context.ApplicationType.FindAsync(id);
            if (applicationType == null)
            {
                return NotFound();
            }

            _context.ApplicationType.Remove(applicationType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationTypeExists(int id)
        {
            return _context.ApplicationType.Any(e => e.application_type_id == id);
        }
    }
}
