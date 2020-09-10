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
    public class ProceduresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProceduresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Procedures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Procedures>>> GetProcedures()
        {
            return await _context.Procedures.ToListAsync();
        }

        // GET: api/Procedures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Procedures>> GetProcedures(int id)
        {
            var procedures = await _context.Procedures.FindAsync(id);

            if (procedures == null)
            {
                return NotFound();
            }

            return procedures;
        }

        // PUT: api/Procedures/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcedures(int id, Procedures procedures)
        {
            if (id != procedures.Id)
            {
                return BadRequest();
            }

            _context.Entry(procedures).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProceduresExists(id))
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

        // POST: api/Procedures
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Procedures>> PostProcedures(Procedures procedures)
        {
            _context.Procedures.Add(procedures);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProcedures", new { id = procedures.Id }, procedures);
        }

        // DELETE: api/Procedures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Procedures>> DeleteProcedures(int id)
        {
            var procedures = await _context.Procedures.FindAsync(id);
            if (procedures == null)
            {
                return NotFound();
            }

            _context.Procedures.Remove(procedures);
            await _context.SaveChangesAsync();

            return procedures;
        }

        private bool ProceduresExists(int id)
        {
            return _context.Procedures.Any(e => e.Id == id);
        }
    }
}
