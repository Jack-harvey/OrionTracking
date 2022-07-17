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
using OrionTracking.Library;
using Microsoft.AspNetCore.Identity;
using OrionTracking.Areas.Identity.Data;

namespace OrionTracking.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly OrionContext _context;
        private readonly IAuditUtilities _auditUtilities;
        private readonly UserManager<ApplicationUser> _userManager;


        public EmployeesController(OrionContext context, IAuditUtilities auditUtilities, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _auditUtilities = auditUtilities;
            _userManager = userManager;
        }


        public async Task TestAudit(string currentUser)
        {
            //var currentUser = await _userManager.GetUserAsync(User);
            //ApplicationUser currentUser = new ApplicationUser();
            await _auditUtilities.WriteAuditData("testCol", 1, "OLD", "NEW", currentUser, "");
        }


        // GET: Employees
        public async Task<IActionResult> Index()
        {
            //var orionContext = _context.Employees.Include(e => e.Company).Include(e => e.CompanyDivision).Include(e => e.JobTitle).Include(e => e.Manager).Include(e => e.Office);
            //return View(await orionContext.ToListAsync());
            return View();
        }

        //GET: index table for DevExtreme
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

            string currentUser = _userManager.GetUserId(User);
            if (currentUser != null)
            {
                //await TestAudit(currentUser);
                //await _auditUtilities.WriteAuditData("testCol", 1, "OLD", "NEW", currentUser, "xlkjas");


            }

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
            ViewData["CompanyId"] = new SelectList(_context.Companies.OrderBy(o => o.Name), "Id", "Name");
            ViewData["CompanyDivisionId"] = new SelectList(_context.CompanyDivisions.OrderBy(o => o.Name), "Id", "Name");
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles.OrderBy(o => o.Name), "Id", "Name");
            ViewData["ManagerId"] = new SelectList(_context.Employees.OrderBy(o => o.FirstName), "Id", "FirstName");
            ViewData["OfficeId"] = new SelectList(_context.Offices.OrderBy(o => o.Company.Name).Select(s => new { s.Id, s.Address }), "Id", "Address");
            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies.OrderBy(o => o.Name), "Id", "Name");
            ViewData["CompanyDivisionId"] = new SelectList(_context.CompanyDivisions.OrderBy(o => o.Name), "Id", "Name");
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles.OrderBy(o => o.Name), "Id", "Name");
            ViewData["ManagerId"] = new SelectList(_context.Employees.OrderBy(o => o.FirstName), "Id", "FirstName");
            ViewData["OfficeId"] = new SelectList(_context.Offices.OrderBy(o => o.Company.Name).Select(s => new {s.Id, s.Address}), "Id", "Address");
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
            ViewData["CompanyId"] = new SelectList(_context.Companies.OrderBy(o => o.Name), "Id", "Name");
            ViewData["CompanyDivisionId"] = new SelectList(_context.CompanyDivisions.OrderBy(o => o.Name), "Id", "Name");
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles.OrderBy(o => o.Name), "Id", "Name");
            ViewData["ManagerId"] = new SelectList(_context.Employees.OrderBy(o => o.FirstName), "Id", "FirstName");
            ViewData["OfficeId"] = new SelectList(_context.Offices.OrderBy(o => o.Company.Name).Select(s => new { s.Id, s.Address }), "Id", "Address");
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

            ViewData["CompanyId"] = new SelectList(_context.Companies.OrderBy(o => o.Name), "Id", "Name");
            ViewData["CompanyDivisionId"] = new SelectList(_context.CompanyDivisions.OrderBy(o => o.Name), "Id", "Name");
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles.OrderBy(o => o.Name), "Id", "Name");
            ViewData["ManagerId"] = new SelectList(_context.Employees.OrderBy(o => o.FirstName), "Id", "FirstName");
            ViewData["OfficeId"] = new SelectList(_context.Offices.OrderBy(o => o.Company.Name).Select(s => new { s.Id, s.Address }), "Id", "Address");
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
