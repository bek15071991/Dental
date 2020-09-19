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
    [Route("api/paysetups")]
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
        public async Task<ActionResult<IEnumerable<PaySetup>>> GetPaySetups()
        {
            return await _context.PaySetups.ToListAsync();
        }

        // GET: api/PaySetups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaySetup>> GetPaySetup(int id)
        {
            var paySetup = await _context.PaySetups.FindAsync(id);

            if (paySetup == null)
            {
                return NotFound();
            }

            return paySetup;
        }

        // PUT: api/PaySetups/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaySetup(int id, PaySetup paySetup)
        {
            if (id != paySetup.Id)
            {
                return BadRequest();
            }

            _context.Entry(paySetup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaySetupExists(id))
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
        public async Task<ActionResult<PaySetup>> PostPaySetup(PaySetup paySetup)
        {
            _context.PaySetups.Add(paySetup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaySetup", new { id = paySetup.Id }, paySetup);
        }

        // DELETE: api/PaySetups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaySetup>> DeletePaySetup(int id)
        {
            var paySetup = await _context.PaySetups.FindAsync(id);
            if (paySetup == null)
            {
                return NotFound();
            }

            _context.PaySetups.Remove(paySetup);
            await _context.SaveChangesAsync();

            return paySetup;
        }

        private bool PaySetupExists(int id)
        {
            return _context.PaySetups.Any(e => e.Id == id);
        }
    }
}
