﻿using System;
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
            try
            {
                _context = context;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        // GET: api/IpdmsUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IpdmsUser>>> GetIpdmsUser()
        {
            return await _context.IpdmsUser.ToListAsync();
        }

        [HttpGet("GetUserListTable")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetUserListTable()
        {
            var result = await (from u in _context.IpdmsUser
                                select new
                                {
                                    Name = new { id = u.ipdms_user_id,lastname = u.last_name, firstname = u.first_name, middlename = u.middle_name },
                                    Email = u.email,
                                    Role = new { id = u.user_role_id, desc = u.user_role_id == 1 ? "Super Admin" : u.user_role_id == 2 ? "Admin" : "Agent" },
                                    Actions = new { userId = u.ipdms_user_id }
                                }).ToListAsync();

            return result;
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
        // To protect from overposting attacks, enable the specific properties you wnpm ant to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIpdmsUser(int id, IpdmsUser ipdmsUser)
        {
            if (id != ipdmsUser.ipdms_user_id)
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
            var result = await _context.IpdmsUser.AnyAsync(u => u.email == ipdmsUser.email);
            if (!result)
            {
                _context.IpdmsUser.Add(ipdmsUser);
                await _context.SaveChangesAsync();
            }
            

            return CreatedAtAction("GetIpdmsUser", new { id = ipdmsUser.ipdms_user_id }, ipdmsUser);
        }

        //POST: api/IpdmsUsers/signin
        [HttpPost("signin")]
        public async Task<ActionResult<IpdmsUser>> ValidateCredential(IpdmsUser ipdmsUser)
        {
            IpdmsUser a = new IpdmsUser();
            var result = await _context.IpdmsUser.AnyAsync(u => u.email == ipdmsUser.email && u.password == ipdmsUser.password);
            if (result)
            {
                a = await (from u in _context.IpdmsUser
                           where u.email == ipdmsUser.email && u.password == ipdmsUser.password
                           select new IpdmsUser
                           {
                               ipdms_user_id = u.ipdms_user_id,
                               first_name = u.first_name,
                               middle_name = u.middle_name,
                               last_name = u.last_name,
                               user_role_id = u.user_role_id,
                               email = u.email,
                               password = u.password,
                               CREATE_USER_ID = u.CREATE_USER_ID,
                               CREATE_USER_DATE = u.CREATE_USER_DATE,
                               LAST_UPDATE_USER_ID = u.LAST_UPDATE_USER_ID,
                               LAST_UPDATE_USER_DATE = u.LAST_UPDATE_USER_DATE
                           }).DefaultIfEmpty().FirstOrDefaultAsync();
            }
            return a;
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
            return _context.IpdmsUser.Any(e => e.ipdms_user_id == id);
        }
    }
}
