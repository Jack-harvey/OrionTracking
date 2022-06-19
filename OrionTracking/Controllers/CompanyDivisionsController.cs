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
    public class CompanyDivisionsController : Controller
    {
        private readonly OrionContext _context;

        public CompanyDivisionsController(OrionContext context)
        {
            _context = context;
        }

        // GET: CompanyDivisions
        public async Task<IActionResult> Index()
        {
            var orionContext = _context.CompanyDivisions.Include(c => c.Company);
            return View(await orionContext.ToListAsync());
        }

        // GET: CompanyDivisions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CompanyDivisions == null)
            {
                return NotFound();
            }

            var companyDivision = await _context.CompanyDivisions
                .Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyDivision == null)
            {
                return NotFound();
            }

            return View(companyDivision);
        }

        // GET: CompanyDivisions/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            return View();
        }

        // POST: CompanyDivisions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CompanyId,ManagerId")] CompanyDivision companyDivision)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyDivision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", companyDivision.CompanyId);
            return View(companyDivision);
        }

        // GET: CompanyDivisions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CompanyDivisions == null)
            {
                return NotFound();
            }

            var companyDivision = await _context.CompanyDivisions.FindAsync(id);
            if (companyDivision == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", companyDivision.CompanyId);
            return View(companyDivision);
        }

        // POST: CompanyDivisions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CompanyId,ManagerId")] CompanyDivision companyDivision)
        {
            if (id != companyDivision.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyDivision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyDivisionExists(companyDivision.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", companyDivision.CompanyId);
            return View(companyDivision);
        }

        // GET: CompanyDivisions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CompanyDivisions == null)
            {
                return NotFound();
            }

            var companyDivision = await _context.CompanyDivisions
                .Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyDivision == null)
            {
                return NotFound();
            }

            return View(companyDivision);
        }

        // POST: CompanyDivisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CompanyDivisions == null)
            {
                return Problem("Entity set 'OrionContext.CompanyDivisions'  is null.");
            }
            var companyDivision = await _context.CompanyDivisions.FindAsync(id);
            if (companyDivision != null)
            {
                _context.CompanyDivisions.Remove(companyDivision);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyDivisionExists(int id)
        {
          return (_context.CompanyDivisions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
