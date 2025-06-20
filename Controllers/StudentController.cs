using System.Linq.Dynamic.Core;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Data;
using StudentManagementApp.Models;
using StudentManagementApp.Services;
using StudentManagementApp.ViewModels;

namespace StudentManagementApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly IExportService exportService;

        public StudentController(AppDbContext appDbContext, IExportService exportService)
        {
            this.appDbContext = appDbContext;
            this.exportService = exportService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StudentViewModel studentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(studentViewModel);
            }

            var student = new Student
            {
                StudentNumber = studentViewModel.StudentNumber,
                FirstName = studentViewModel.FirstName,
                LastName = studentViewModel.LastName,
                EmailAddress = studentViewModel.EmailAddress,
                DateOfBirth = DateTime.SpecifyKind(studentViewModel.DateOfBirth, DateTimeKind.Utc),
            };

            await appDbContext.AddAsync(student);
            await appDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        //GET: /Student/View
        // public async Task<IActionResult> ViewStudents()
        // {
        //     var students = await appDbContext.Students.ToListAsync();
        //     return View("View", students);
        // }

        public IActionResult ViewStudents()
        {
            return View("View");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> GetStudents([FromBody] DataTableRequest request)
        {
            try
            {
                var query = appDbContext.Students.AsQueryable();

                var totalRecords = await query.CountAsync();

                if (!string.IsNullOrEmpty(request.Search.Value))
                {
                    string searchValue = request.Search.Value.ToLower();
                    query = query.Where(s =>
                        s.StudentNumber.Contains(searchValue)
                        || s.FirstName.Contains(searchValue)
                        || s.LastName.Contains(searchValue)
                        || s.EmailAddress.Contains(searchValue)
                    );
                }

                var filteredRecords = await query.CountAsync();

                if (request.Order.Any())
                {
                    var order = request.Order.First();
                    var columnName = request.Columns[order.Column].Data;
                    var sortDirection = order.Dir == "asc" ? "ascending" : "descending";

                    query = columnName switch
                    {
                        "firstName" => order.Dir == "asc"
                            ? query.OrderBy(s => s.FirstName)
                            : query.OrderByDescending(s => s.FirstName),
                        "lastName" => order.Dir == "asc"
                            ? query.OrderBy(s => s.LastName)
                            : query.OrderByDescending(s => s.LastName),
                        "emailAddress" => order.Dir == "asc"
                            ? query.OrderBy(s => s.EmailAddress)
                            : query.OrderByDescending(s => s.EmailAddress),
                        "dateOfBirth" => order.Dir == "asc"
                            ? query.OrderBy(s => s.DateOfBirth)
                            : query.OrderByDescending(s => s.DateOfBirth),
                        _ => query.OrderBy(s => s.StudentNumber),
                    };
                }
                else
                {
                    query = query.OrderBy(s => s.StudentNumber);
                }

                var students = await query
                    .Skip(request.Start)
                    .Take(request.Length)
                    .Select(s => new StudentViewModel
                    {
                        StudentNumber = s.StudentNumber,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        EmailAddress = s.EmailAddress,
                        DateOfBirth = s.DateOfBirth,
                    })
                    .ToListAsync();

                var response = new DataTableResponse<StudentViewModel>
                {
                    Draw = request.Draw,
                    RecordsFiltered = filteredRecords,
                    RecordsTotal = totalRecords,
                    Data = students,
                };
                return Json(response);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        //GET: /Student/EditSearch
        [HttpGet]
        public IActionResult EditSearch()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSearch(string StudentNumber)
        {
            if (string.IsNullOrEmpty(StudentNumber))
            {
                ModelState.AddModelError("", "Please provide a student number");
                return View();
            }

            var student = await appDbContext.Students.FirstOrDefaultAsync(x =>
                x.StudentNumber == StudentNumber
            );
            if (student == null)
            {
                ModelState.AddModelError("", "Student not found");
                return View();
            }

            return RedirectToAction("Edit", new { studentNumber = student.StudentNumber });
        }

        //GET: /Student/Edit/{studentNumber}
        [HttpGet]
        public async Task<IActionResult> Edit(string studentNumber)
        {
            var student = await appDbContext.Students.FirstOrDefaultAsync(x =>
                x.StudentNumber == studentNumber
            );

            if (student == null)
            {
                return NotFound();
            }

            var studentViewModel = new StudentViewModel
            {
                StudentNumber = student.StudentNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                EmailAddress = student.EmailAddress,
                DateOfBirth = student.DateOfBirth,
            };

            return View(studentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentViewModel studentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(studentViewModel);
            }

            var student = await appDbContext.Students.FirstOrDefaultAsync(x =>
                x.StudentNumber == studentViewModel.StudentNumber
            );

            if (student == null)
            {
                return NotFound();
            }

            student.FirstName = studentViewModel.FirstName;
            student.LastName = studentViewModel.LastName;
            student.EmailAddress = studentViewModel.EmailAddress;
            student.DateOfBirth = studentViewModel.DateOfBirth;

            await appDbContext.SaveChangesAsync();

            return RedirectToAction("ViewStudents");
        }

        //GET: /Student/DeleteSearch
        [HttpGet]
        public IActionResult DeleteSearch()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSearch(string studentNumber)
        {
            if (string.IsNullOrEmpty(studentNumber))
            {
                ModelState.AddModelError("", "Please provide a student number");
                return View();
            }

            var student = await appDbContext.Students.FirstOrDefaultAsync(x =>
                x.StudentNumber == studentNumber
            );
            if (student == null)
            {
                ModelState.AddModelError("", "Student is not found");
            }

            return RedirectToAction("Delete", new { studentNumber = student.StudentNumber });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string studentNumber)
        {
            var student = await appDbContext.Students.FirstOrDefaultAsync(x =>
                x.StudentNumber == studentNumber
            );

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string studentNumber)
        {
            var student = await appDbContext.Students.FirstOrDefaultAsync(x =>
                x.StudentNumber == studentNumber
            );
            if (student == null)
            {
                return NotFound();
            }

            appDbContext.Students.Remove(student);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("ViewStudents");
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                var students = await appDbContext
                    .Students.OrderBy(s => s.StudentNumber)
                    .ToListAsync();

                var content = exportService.ExportToExcel(students);
                var fileName = $"Student_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                return File(
                    content,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName
                );
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error exporting to excel: {ex.Message}";
                return RedirectToAction("View");
            }
        }

        public async Task<IActionResult> ExportToPdf()
        {
            var students = await appDbContext.Students.OrderBy(e => e.StudentNumber).ToListAsync();

            var pdfBytes = exportService.ExportToPdf(students);
            return File(pdfBytes, "application/pdf", $"Students_{DateTime.Now:yyyyMMdd}.pdf");
        }
    }
}
