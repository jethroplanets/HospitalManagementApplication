using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalMgt.Data;
using HospitalMgt.Models;

namespace HospitalMgt
{
    public class Dispenses1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Dispenses1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dispenses1
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Dispense.Include(d => d.Doctor).Include(d => d.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Dispenses1/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispense = await _context.Dispense
                .Include(d => d.Doctor)
                .Include(d => d.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dispense == null)
            {
                return NotFound();
            }

            return View(dispense);
        }

        // GET: Dispenses1/Create
        public IActionResult Create()
        {
            ViewData["DispensedBy"] = new SelectList(_context.Doctor, "Id", "Id");
            ViewData["DispensedTo"] = new SelectList(_context.Patient, "Id", "Id");
            return View();
        }

        // POST: Dispenses1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DispensedBy,DispensedTo")] Dispense dispense)
        {
            if (ModelState.IsValid)
            {
                dispense.Id = Guid.NewGuid();
                _context.Add(dispense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DispensedBy"] = new SelectList(_context.Doctor, "Id", "Id", dispense.DispensedBy);
            ViewData["DispensedTo"] = new SelectList(_context.Patient, "Id", "Id", dispense.DispensedTo);
            return View(dispense);
        }

        // GET: Dispenses1/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispense = await _context.Dispense.FindAsync(id);
            if (dispense == null)
            {
                return NotFound();
            }
            ViewData["DispensedBy"] = new SelectList(_context.Doctor, "Id", "Id", dispense.DispensedBy);
            ViewData["DispensedTo"] = new SelectList(_context.Patient, "Id", "Id", dispense.DispensedTo);
            return View(dispense);
        }

        // POST: Dispenses1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DispensedBy,DispensedTo")] Dispense dispense)
        {
            if (id != dispense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dispense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DispenseExists(dispense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DispensedBy"] = new SelectList(_context.Doctor, "Id", "Id", dispense.DispensedBy);
            ViewData["DispensedTo"] = new SelectList(_context.Patient, "Id", "Id", dispense.DispensedTo);
            return View(dispense);
        }

        // GET: Dispenses1/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispense = await _context.Dispense
                .Include(d => d.Doctor)
                .Include(d => d.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dispense == null)
            {
                return NotFound();
            }

            return View(dispense);
        }

        // POST: Dispenses1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dispense = await _context.Dispense.FindAsync(id);
            _context.Dispense.Remove(dispense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DispenseExists(Guid id)
        {
            return _context.Dispense.Any(e => e.Id == id);
        }
    }
}
