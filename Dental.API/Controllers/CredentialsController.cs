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
    [Route("api/credentials")]
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
        public async Task<ActionResult<IEnumerable<Credential>>> GetCredentiatials()
        {
            return await _context.Credentiatials.ToListAsync();
        }

        // GET: api/Credentials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Credential>> GetCredential(int id)
        {
            var credential = await _context.Credentiatials.FindAsync(id);

            if (credential == null)
            {
                return NotFound();
            }

            return credential;
        }

        // PUT: api/Credentials/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCredential(int id, Credential credential)
        {
            if (id != credential.Id)
            {
                return BadRequest();
            }

            _context.Entry(credential).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CredentialExists(id))
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
        public async Task<ActionResult<Credential>> PostCredential(Credential credential)
        {
            _context.Credentiatials.Add(credential);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCredential", new { id = credential.Id }, credential);
        }

        // DELETE: api/Credentials/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Credential>> DeleteCredential(int id)
        {
            var credential = await _context.Credentiatials.FindAsync(id);
            if (credential == null)
            {
                return NotFound();
            }

            _context.Credentiatials.Remove(credential);
            await _context.SaveChangesAsync();

            return credential;
        }

        private bool CredentialExists(int id)
        {
            return _context.Credentiatials.Any(e => e.Id == id);
        }
    }
}
