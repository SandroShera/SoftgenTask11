using Microsoft.AspNetCore.Mvc;
using SoftgenTask11.Data;
using SoftgenTask11.Models;
using SoftgenTask11.ViewModels;

namespace SoftgenTask11.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult StudentIndex()
        {
            var model = _context.Students;
            return View(model);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(StudentCreateViewModel model)
        {
            Student student = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PersonalNumber = model.PersonalNumber,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
            };

            _context.Add(student);
            _context.SaveChanges();

            return RedirectToAction("StudentIndex");
        }

        [HttpPost]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            _context.Students.Remove(student);

            _context.SaveChanges();

            return RedirectToAction("StudentIndex");
        }
    }
}
