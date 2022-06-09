using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrionTracking.Data;
using OrionTracking.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using OrionTracking.Models.Binding;

namespace OrionTracking.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly OrionContext _context;

        public EmployeesController(OrionContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            //var orionContext = _context.Employees.Include(e => e.Company).Include(e => e.CompanyDivision).Include(e => e.JobTitle).Include(e => e.Manager).Include(e => e.Office);
            //return View(await orionContext.ToListAsync());
            return View();
        }

        //[HttpGet("employees")]
        [HttpGet]
        public async Task<IActionResult> GetAction(DevExtremeDataSourceLoadOptions loadOptions)
        {
            var source = _context.Employees.Select(o => new
            {
                o.Id,
                o.FirstName,
                o.LastName,
                o.StartDate,
                o.Email,
                o.Office.City,
                o.JobTitle.Name,
                o.UserName
            });

            loadOptions.PrimaryKey = new[] { "Id" };
            loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(source, loadOptions));

        }




        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Company)
                .Include(e => e.CompanyDivision)
                .Include(e => e.JobTitle)
                .Include(e => e.Manager)
                .Include(e => e.Office)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            ViewData["CompanyDivisionId"] = new SelectList(_context.CompanyDivisions, "Id", "Id");
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles, "Id", "Id");
            ViewData["ManagerId"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Id");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,UserName,ManagerId,Active,Notes,StartDate,EndDate,Email,JobTitleId,CompanyDivisionId,CompanyId,OfficeId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", employee.CompanyId);
            ViewData["CompanyDivisionId"] = new SelectList(_context.CompanyDivisions, "Id", "Id", employee.CompanyDivisionId);
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles, "Id", "Id", employee.JobTitleId);
            ViewData["ManagerId"] = new SelectList(_context.Employees, "Id", "Id", employee.ManagerId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Id", employee.OfficeId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", employee.CompanyId);
            ViewData["CompanyDivisionId"] = new SelectList(_context.CompanyDivisions, "Id", "Id", employee.CompanyDivisionId);
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles, "Id", "Id", employee.JobTitleId);
            ViewData["ManagerId"] = new SelectList(_context.Employees, "Id", "Id", employee.ManagerId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Id", employee.OfficeId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,UserName,ManagerId,Active,Notes,StartDate,EndDate,Email,JobTitleId,CompanyDivisionId,CompanyId,OfficeId")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", employee.CompanyId);
            ViewData["CompanyDivisionId"] = new SelectList(_context.CompanyDivisions, "Id", "Id", employee.CompanyDivisionId);
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles, "Id", "Id", employee.JobTitleId);
            ViewData["ManagerId"] = new SelectList(_context.Employees, "Id", "Id", employee.ManagerId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Id", employee.OfficeId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Company)
                .Include(e => e.CompanyDivision)
                .Include(e => e.JobTitle)
                .Include(e => e.Manager)
                .Include(e => e.Office)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'OrionContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
