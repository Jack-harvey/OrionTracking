using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using FileSignatures;
using FileSignatures.Formats;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrionTracking.Data;
using OrionTracking.Models;
using OrionTracking.Models.Binding;

namespace OrionTracking.Controllers
{
    public class EmployeeDocumentsController : Controller
    {
        private readonly OrionContext _context;
        private readonly IConfiguration _configuration;

        public EmployeeDocumentsController(OrionContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //Method to save the document to requested path
        public async Task DocumentSaver(IFormFile pdfUpload, string uniqueFileName, string uniquePath)
        {
            string fullDirectory = $"{_configuration.GetValue<string>("OrionConfigurationSettings:RootDocumentPath")}" + $"{uniquePath}";
            if (!Directory.Exists(fullDirectory))
            Directory.CreateDirectory(fullDirectory);

            using (FileStream fileStream = new($"{fullDirectory}" + $"{uniqueFileName}", FileMode.Create))
            {
                await pdfUpload.CopyToAsync(fileStream);

            }
            return;
        }


        public bool IsFileValidPdf(IFormFile pdfUpload)
        {
            return new FileFormatInspector().DetermineFileFormat(pdfUpload.OpenReadStream()) is Pdf;
            //var inspector = new FileFormatInspector();
            //var fileTypeInspection = inspector.DetermineFileFormat(pdfUpload.OpenReadStream());
            //bool isValidPdf = fileTypeInspection is Pdf;
            //return isValidPdf;
        }

        // GET: EmployeeDocuments
        public async Task<IActionResult> Index()
        {
            //var orionContext = _context.EmployeeDocuments.Include(e => e.Employee).Include(e => e.Type);
            //return View(await orionContext.ToListAsync());
            return View();
        }

        //GET: Asset table for DevExtreme
        [HttpGet]
        public async Task<IActionResult> GetAction(DevExtremeDataSourceLoadOptions loadOptions)
        {
            var source = _context.EmployeeDocuments.Select(o => new
            {
                o.Id,
                o.Name,
                o.Path,
                o.Timestamp,
                docTypeName = o.Type.Name,
                o.Employee.UserName
            });

            loadOptions.PrimaryKey = new[] { "id" };
            loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(source, loadOptions));
        }

        // GET: EmployeeDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeDocuments == null)
            {
                return NotFound();
            }

            var employeeDocument = await _context.EmployeeDocuments
                .Include(e => e.Employee)
                .Include(e => e.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeDocument == null)
            {
                return NotFound();
            }

            return View(employeeDocument);
        }

        // GET: EmployeeDocuments/Create
        public IActionResult Create()
        {
            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["EmployeeId"] = new SelectList(_context.Employees.OrderBy(o => o.UserName).Select(s => new { s.Id, s.UserName }), "Id", "UserName");
            //ViewData["TypeId"] = new SelectList(_context.DocumentTypes, "Id", "Id");
            ViewData["TypeId"] = new SelectList(_context.DocumentTypes.OrderBy(o => o.Name).Select(s => new { s.Id, s.Name }), "Id", "Name");
            return View();
        }

        // POST: EmployeeDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Path,Timestamp,TypeId,EmployeeId")] EmployeeDocument employeeDocument, IFormFile pdfUpload)
        {
            var modelStateErrors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                bool employeeAndDocumentValid = true;
                DateTime uploadTimeStamp = DateTime.Now;
                Employee? selectedEmployee = _context.Employees.FirstOrDefault(o => o.Id == employeeDocument.EmployeeId);
                DocumentType? selectedDocument = _context.DocumentTypes.FirstOrDefault(o => o.Id == employeeDocument.TypeId);


                if (selectedDocument == null || selectedEmployee == null)
                {
                    employeeAndDocumentValid = false;
                }

                bool fileTypeValid = false;

                if (pdfUpload != null)
                {
                    fileTypeValid = IsFileValidPdf(pdfUpload);
                }
                else
                {
                    fileTypeValid = true;
                }

                try
                {

                    if (fileTypeValid && employeeAndDocumentValid)
                    {
                        if (pdfUpload != null)
                        {
                            string uniqueFolderStructure = $"/{employeeDocument.EmployeeId}/{selectedDocument.Name}/";
                            string uniqueFileName = $"{selectedEmployee.UserName}" + " - " + $"{uploadTimeStamp.ToString("yyyy.MM.dd.HH.mm.ss.ffffff")}" + ".pdf";
                            employeeDocument.Name = $"{selectedEmployee.FirstName}" + $"{selectedEmployee.LastName}" + "-" + $"{selectedDocument.Name}" + $"{uploadTimeStamp.ToString("yyyy.MM.dd.HH.mm.ss.ffffff")}";
                            employeeDocument.Path = $"{uniqueFolderStructure}" + $"{uniqueFileName}";
                            await DocumentSaver(pdfUpload, uniqueFileName, uniqueFolderStructure);
                        }
                        else
                        {
                            employeeDocument.Name = "No document provided";
                            employeeDocument.Path = "No document provided";
                        }

                        employeeDocument.Timestamp = uploadTimeStamp;
                        _context.Add(employeeDocument);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                }
                catch (DbUpdateException employeeDocumentsCreatePost)
                {
                    //_logger.LogError(employeeDocumentsCreatePost, "failed :)";
                    //ModelState.AddModelError("", "Unable to save changes. " +
                    // "Try again, and if the problem persists " +
                    // "see your system administrator.");
                }
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", employeeDocument.EmployeeId);
            ViewData["TypeId"] = new SelectList(_context.DocumentTypes, "Id", "Id", employeeDocument.TypeId);
            return View(employeeDocument);
        }

        // GET: EmployeeDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeDocuments == null)
            {
                return NotFound();
            }

            var employeeDocument = await _context.EmployeeDocuments.FindAsync(id);
            if (employeeDocument == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", employeeDocument.EmployeeId);
            ViewData["TypeId"] = new SelectList(_context.DocumentTypes, "Id", "Id", employeeDocument.TypeId);
            return View(employeeDocument);
        }

        // POST: EmployeeDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Path,Timestamp,TypeId,EmployeeId")] EmployeeDocument employeeDocument)
        {
            if (id != employeeDocument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeDocumentExists(employeeDocument.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", employeeDocument.EmployeeId);
            ViewData["TypeId"] = new SelectList(_context.DocumentTypes, "Id", "Id", employeeDocument.TypeId);
            return View(employeeDocument);
        }

        // GET: EmployeeDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeDocuments == null)
            {
                return NotFound();
            }

            var employeeDocument = await _context.EmployeeDocuments
                .Include(e => e.Employee)
                .Include(e => e.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeDocument == null)
            {
                return NotFound();
            }

            return View(employeeDocument);
        }

        // POST: EmployeeDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeDocuments == null)
            {
                return Problem("Entity set 'OrionContext.EmployeeDocuments'  is null.");
            }
            var employeeDocument = await _context.EmployeeDocuments.FindAsync(id);
            if (employeeDocument != null)
            {
                _context.EmployeeDocuments.Remove(employeeDocument);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeDocumentExists(int id)
        {
            return (_context.EmployeeDocuments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
