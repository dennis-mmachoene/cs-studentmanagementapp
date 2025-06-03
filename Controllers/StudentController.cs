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
    }
}
