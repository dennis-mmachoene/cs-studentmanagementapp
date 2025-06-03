using Microsoft.AspNetCore.Mvc;
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
    }
}
