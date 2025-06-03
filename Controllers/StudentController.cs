using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Data;
using StudentManagementApp.Models;
using StudentManagementApp.ViewModels;

namespace StudentManagementApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext appDbContext;

        public StudentController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
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
        public async Task<IActionResult> ViewStudents()
        {
            var students = await appDbContext.Students.ToListAsync();
            return View("View", students);
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
            var student = await appDbContext.Students.FirstOrDefaultAsync(x => x.StudentNumber == studentNumber);
            if (student == null)
            {
                return NotFound();
            }

            appDbContext.Students.Remove(student);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("ViewStudents");
        }
    }
}
