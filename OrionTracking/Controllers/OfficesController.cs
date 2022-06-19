using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrionTracking.Data;
using OrionTracking.Models;

namespace OrionTracking.Controllers
{
    public class OfficesController : Controller
    {
        private readonly OrionContext _context;

        public OfficesController(OrionContext context)
        {
            _context = context;
        }

        // GET: Offices
        public async Task<IActionResult> Index()
        {
            var orionContext = _context.Offices.Include(o => o.Company);
            return View(await orionContext.ToListAsync());
        }

        // GET: Offices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Offices == null)
            {
                return NotFound();
            }

            var office = await _context.Offices
                .Include(o => o.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (office == null)
            {
                return NotFound();
            }

            return View(office);
        }

        // GET: Offices/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            return View();
        }

        // POST: Offices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,State,PostCode,City,Address,PhoneNumber,PoboxNumber,CompanyId")] Office office)
        {
            if (ModelState.IsValid)
            {
                _context.Add(office);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", office.CompanyId);
            return View(office);
        }

        // GET: Offices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Offices == null)
            {
                return NotFound();
            }

            var office = await _context.Offices.FindAsync(id);
            if (office == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", office.CompanyId);
            return View(office);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,State,PostCode,City,Address,PhoneNumber,PoboxNumber,CompanyId")] Office office)
        {
            if (id != office.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(office);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficeExists(office.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", office.CompanyId);
            return View(office);
        }

        // GET: Offices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Offices == null)
            {
                return NotFound();
            }

            var office = await _context.Offices
                .Include(o => o.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (office == null)
            {
                return NotFound();
            }

            return View(office);
        }

        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Offices == null)
            {
                return Problem("Entity set 'OrionContext.Offices'  is null.");
            }
            var office = await _context.Offices.FindAsync(id);
            if (office != null)
            {
                _context.Offices.Remove(office);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfficeExists(int id)
        {
          return (_context.Offices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
