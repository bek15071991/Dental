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
    public class PaySetupsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaySetupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PaySetups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaySetups>>> GetPaySetups()
        {
            return await _context.PaySetups.ToListAsync();
        }

        // GET: api/PaySetups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaySetups>> GetPaySetups(int id)
        {
            var paySetups = await _context.PaySetups.FindAsync(id);

            if (paySetups == null)
            {
                return NotFound();
            }

            return paySetups;
        }

        // PUT: api/PaySetups/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaySetups(int id, PaySetups paySetups)
        {
            if (id != paySetups.Id)
            {
                return BadRequest();
            }

            _context.Entry(paySetups).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaySetupsExists(id))
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

        // POST: api/PaySetups
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PaySetups>> PostPaySetups(PaySetups paySetups)
        {
            _context.PaySetups.Add(paySetups);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaySetups", new { id = paySetups.Id }, paySetups);
        }

        // DELETE: api/PaySetups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaySetups>> DeletePaySetups(int id)
        {
            var paySetups = await _context.PaySetups.FindAsync(id);
            if (paySetups == null)
            {
                return NotFound();
            }

            _context.PaySetups.Remove(paySetups);
            await _context.SaveChangesAsync();

            return paySetups;
        }

        private bool PaySetupsExists(int id)
        {
            return _context.PaySetups.Any(e => e.Id == id);
        }
    }
}
