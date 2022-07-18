using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrionTracking.Data;
using OrionTracking.Models;
using OrionTracking.Models.Binding;

namespace OrionTracking.Controllers
{
    public class ModelManufacturersController : Controller
    {
        private readonly OrionContext _context;

        public ModelManufacturersController(OrionContext context)
        {
            _context = context;
        }

        // GET: ModelManufacturers
        public async Task<IActionResult> Index()
        {
              //return _context.ModelManufacturers != null ? 
              //            View(await _context.ModelManufacturers.ToListAsync()) :
              //            Problem("Entity set 'OrionContext.ModelManufacturers'  is null.");
              return View();
        }

        //GET: Asset table for DevExtreme
        [HttpGet]
        public async Task<IActionResult> GetAction(DevExtremeDataSourceLoadOptions loadOptions)
        {
            var source = _context.ModelManufacturers.Select(o => new
            {
                o.Id,
                o.Name
            });

            loadOptions.PrimaryKey = new[] { "id" };
            loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(source, loadOptions));
        }

        // GET: ModelManufacturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ModelManufacturers == null)
            {
                return NotFound();
            }

            var modelManufacturer = await _context.ModelManufacturers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelManufacturer == null)
            {
                return NotFound();
            }

            return View(modelManufacturer);
        }

        // GET: ModelManufacturers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModelManufacturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ModelManufacturer modelManufacturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelManufacturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modelManufacturer);
        }

        // GET: ModelManufacturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ModelManufacturers == null)
            {
                return NotFound();
            }

            var modelManufacturer = await _context.ModelManufacturers.FindAsync(id);
            if (modelManufacturer == null)
            {
                return NotFound();
            }
            return View(modelManufacturer);
        }

        // POST: ModelManufacturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ModelManufacturer modelManufacturer)
        {
            if (id != modelManufacturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelManufacturer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelManufacturerExists(modelManufacturer.Id))
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
            return View(modelManufacturer);
        }

        // GET: ModelManufacturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ModelManufacturers == null)
            {
                return NotFound();
            }

            var modelManufacturer = await _context.ModelManufacturers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelManufacturer == null)
            {
                return NotFound();
            }

            return View(modelManufacturer);
        }

        // POST: ModelManufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ModelManufacturers == null)
            {
                return Problem("Entity set 'OrionContext.ModelManufacturers'  is null.");
            }
            var modelManufacturer = await _context.ModelManufacturers.FindAsync(id);
            if (modelManufacturer != null)
            {
                _context.ModelManufacturers.Remove(modelManufacturer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelManufacturerExists(int id)
        {
          return (_context.ModelManufacturers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
