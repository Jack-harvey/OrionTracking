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
    public class AssetModelsController : Controller
    {
        private readonly OrionContext _context;

        public AssetModelsController(OrionContext context)
        {
            _context = context;
        }

        // GET: AssetModels
        public async Task<IActionResult> Index()
        {
            var orionContext = _context.AssetModels.Include(a => a.Manufacturer);
            return View(await orionContext.ToListAsync());
        }

        // GET: AssetModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AssetModels == null)
            {
                return NotFound();
            }

            var assetModel = await _context.AssetModels
                .Include(a => a.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetModel == null)
            {
                return NotFound();
            }

            return View(assetModel);
        }

        // GET: AssetModels/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.ModelManufacturers, "Id", "Id");
            return View();
        }

        // POST: AssetModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ManufacturerId")] AssetModel assetModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.ModelManufacturers, "Id", "Id", assetModel.ManufacturerId);
            return View(assetModel);
        }

        // GET: AssetModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AssetModels == null)
            {
                return NotFound();
            }

            var assetModel = await _context.AssetModels.FindAsync(id);
            if (assetModel == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.ModelManufacturers, "Id", "Id", assetModel.ManufacturerId);
            return View(assetModel);
        }

        // POST: AssetModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ManufacturerId")] AssetModel assetModel)
        {
            if (id != assetModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetModelExists(assetModel.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.ModelManufacturers, "Id", "Id", assetModel.ManufacturerId);
            return View(assetModel);
        }

        // GET: AssetModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AssetModels == null)
            {
                return NotFound();
            }

            var assetModel = await _context.AssetModels
                .Include(a => a.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetModel == null)
            {
                return NotFound();
            }

            return View(assetModel);
        }

        // POST: AssetModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AssetModels == null)
            {
                return Problem("Entity set 'OrionContext.AssetModels'  is null.");
            }
            var assetModel = await _context.AssetModels.FindAsync(id);
            if (assetModel != null)
            {
                _context.AssetModels.Remove(assetModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetModelExists(int id)
        {
          return (_context.AssetModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
