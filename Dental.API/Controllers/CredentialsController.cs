using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dental.Data.Data;
using Dental.Data.Models;

namespace Dental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CredentialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Credentials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Credentials>>> GetCredentiatials()
        {
            return await _context.Credentiatials.ToListAsync();
        }

        // GET: api/Credentials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Credentials>> GetCredentials(int id)
        {
            var credentials = await _context.Credentiatials.FindAsync(id);

            if (credentials == null)
            {
                return NotFound();
            }

            return credentials;
        }

        // PUT: api/Credentials/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCredentials(int id, Credentials credentials)
        {
            if (id != credentials.Id)
            {
                return BadRequest();
            }

            _context.Entry(credentials).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CredentialsExists(id))
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

        // POST: api/Credentials
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Credentials>> PostCredentials(Credentials credentials)
        {
            _context.Credentiatials.Add(credentials);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCredentials", new { id = credentials.Id }, credentials);
        }

        // DELETE: api/Credentials/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Credentials>> DeleteCredentials(int id)
        {
            var credentials = await _context.Credentiatials.FindAsync(id);
            if (credentials == null)
            {
                return NotFound();
            }

            _context.Credentiatials.Remove(credentials);
            await _context.SaveChangesAsync();

            return credentials;
        }

        private bool CredentialsExists(int id)
        {
            return _context.Credentiatials.Any(e => e.Id == id);
        }
    }
}
