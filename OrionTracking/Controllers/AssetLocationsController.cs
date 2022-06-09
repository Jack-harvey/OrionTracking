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
    public class AssetLocationsController : Controller
    {
        private readonly OrionContext _context;

        public AssetLocationsController(OrionContext context)
        {
            _context = context;
        }

        // GET: AssetLocations
        public async Task<IActionResult> Index()
        {
              return _context.AssetLocations != null ? 
                          View(await _context.AssetLocations.ToListAsync()) :
                          Problem("Entity set 'OrionContext.AssetLocations'  is null.");
        }

        // GET: AssetLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AssetLocations == null)
            {
                return NotFound();
            }

            var assetLocation = await _context.AssetLocations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetLocation == null)
            {
                return NotFound();
            }

            return View(assetLocation);
        }

        // GET: AssetLocations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssetLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,State,PostCode,City,Street,Notes,Active")] AssetLocation assetLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assetLocation);
        }

        // GET: AssetLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AssetLocations == null)
            {
                return NotFound();
            }

            var assetLocation = await _context.AssetLocations.FindAsync(id);
            if (assetLocation == null)
            {
                return NotFound();
            }
            return View(assetLocation);
        }

        // POST: AssetLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,State,PostCode,City,Street,Notes,Active")] AssetLocation assetLocation)
        {
            if (id != assetLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetLocationExists(assetLocation.Id))
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
            return View(assetLocation);
        }

        // GET: AssetLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AssetLocations == null)
            {
                return NotFound();
            }

            var assetLocation = await _context.AssetLocations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetLocation == null)
            {
                return NotFound();
            }

            return View(assetLocation);
        }

        // POST: AssetLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AssetLocations == null)
            {
                return Problem("Entity set 'OrionContext.AssetLocations'  is null.");
            }
            var assetLocation = await _context.AssetLocations.FindAsync(id);
            if (assetLocation != null)
            {
                _context.AssetLocations.Remove(assetLocation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetLocationExists(int id)
        {
          return (_context.AssetLocations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
