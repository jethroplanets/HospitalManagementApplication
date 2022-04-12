using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalMgt.Data;
using HospitalMgt.Models;

namespace HospitalMgt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DispensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Dispenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dispense>>> GetDispense()
        {
            return await _context.Dispense.ToListAsync();
        }

        // GET: api/Dispenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dispense>> GetDispense(Guid id)
        {
            var dispense = await _context.Dispense.FindAsync(id);

            if (dispense == null)
            {
                return NotFound();
            }

            return dispense;
        }

        // PUT: api/Dispenses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDispense(Guid id, Dispense dispense)
        {
            if (id != dispense.Id)
            {
                return BadRequest();
            }

            _context.Entry(dispense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispenseExists(id))
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

        // POST: api/Dispenses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Dispense>> PostDispense(Dispense dispense)
        {
            _context.Dispense.Add(dispense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDispense", new { id = dispense.Id }, dispense);
        }

        // DELETE: api/Dispenses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dispense>> DeleteDispense(Guid id)
        {
            var dispense = await _context.Dispense.FindAsync(id);
            if (dispense == null)
            {
                return NotFound();
            }

            _context.Dispense.Remove(dispense);
            await _context.SaveChangesAsync();

            return dispense;
        }

        private bool DispenseExists(Guid id)
        {
            return _context.Dispense.Any(e => e.Id == id);
        }
    }
}
