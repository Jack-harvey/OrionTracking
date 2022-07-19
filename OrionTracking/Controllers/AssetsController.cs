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
    public class AssetsController : Controller
    {
        private readonly OrionContext _context;

        public AssetsController(OrionContext context)
        {
            _context = context;
        }

        // GET: Assets
        public async Task<IActionResult> Index()
        {
            //var orionContext = _context.Assets.Include(a => a.Employee).Include(a => a.Location).Include(a => a.Model).Include(a => a.ParentAsset).Include(a => a.Type);
            return View();
        }

//GET: Asset table for DevExtreme
        [HttpGet]
        public async Task<IActionResult> GetAction(DevExtremeDataSourceLoadOptions loadOptions)
        {
            //string test = ViewBag.Test;
            var source = _context.Assets.Select(o => new
            {
                o.Id,
                o.CompanyTrackingId,
                o.Name,
                typeName = o.Type.Name,
                o.Employee.UserName,
                Location = o.Location.Name,
                o.PurchaseDate,
                o.Active,
                o.IsMobileService,
                o.MobileNumber,
                ParentAssetId = o.ParentAsset.CompanyTrackingId
            });

            loadOptions.PrimaryKey = new[] { "id" };
            loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(source, loadOptions));
        }

        // GET: Assets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Employee)
                .Include(a => a.Location)
                .Include(a => a.Model)
                .Include(a => a.ParentAsset)
                .Include(a => a.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // GET: Assets/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["LocationId"] = new SelectList(_context.AssetLocations, "Id", "Id");
            ViewData["ModelId"] = new SelectList(_context.AssetModels, "Id", "Id");
            ViewData["ParentAssetId"] = new SelectList(_context.Assets, "Id", "Id");
            ViewData["TypeId"] = new SelectList(_context.AssetTypes, "Id", "Id");
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CompanyTrackingId,SerialNumber,PurchaseDate,PurchaseValue,Active,ModelId,TypeId,EmployeeId,LocationId,ParentAssetId,MobileNumber,IsMobileService,ToBeReturned")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", asset.EmployeeId);
            ViewData["LocationId"] = new SelectList(_context.AssetLocations, "Id", "Id", asset.LocationId);
            ViewData["ModelId"] = new SelectList(_context.AssetModels, "Id", "Id", asset.ModelId);
            ViewData["ParentAssetId"] = new SelectList(_context.Assets, "Id", "Id", asset.ParentAssetId);
            ViewData["TypeId"] = new SelectList(_context.AssetTypes, "Id", "Id", asset.TypeId);
            return View(asset);
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", asset.EmployeeId);
            ViewData["LocationId"] = new SelectList(_context.AssetLocations, "Id", "Id", asset.LocationId);
            ViewData["ModelId"] = new SelectList(_context.AssetModels, "Id", "Id", asset.ModelId);
            ViewData["ParentAssetId"] = new SelectList(_context.Assets, "Id", "Id", asset.ParentAssetId);
            ViewData["TypeId"] = new SelectList(_context.AssetTypes, "Id", "Id", asset.TypeId);
            return View(asset);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CompanyTrackingId,SerialNumber,PurchaseDate,PurchaseValue,Active,ModelId,TypeId,EmployeeId,LocationId,ParentAssetId,MobileNumber,IsMobileService,ToBeReturned")] Asset asset)
        {
            if (id != asset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", asset.EmployeeId);
            ViewData["LocationId"] = new SelectList(_context.AssetLocations, "Id", "Id", asset.LocationId);
            ViewData["ModelId"] = new SelectList(_context.AssetModels, "Id", "Id", asset.ModelId);
            ViewData["ParentAssetId"] = new SelectList(_context.Assets, "Id", "Id", asset.ParentAssetId);
            ViewData["TypeId"] = new SelectList(_context.AssetTypes, "Id", "Id", asset.TypeId);
            return View(asset);
        }

        // GET: Assets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Employee)
                .Include(a => a.Location)
                .Include(a => a.Model)
                .Include(a => a.ParentAsset)
                .Include(a => a.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Assets == null)
            {
                return Problem("Entity set 'OrionContext.Assets'  is null.");
            }
            var asset = await _context.Assets.FindAsync(id);
            if (asset != null)
            {
                _context.Assets.Remove(asset);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(int id)
        {
          return (_context.Assets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
