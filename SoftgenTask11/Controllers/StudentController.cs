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

        public IActionResult StudentIndex(string searchString)
        {
            var model = _context.Students.Where(s => !s.IsDeleted);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredModel = model.Where(x => x.FirstName.Contains(searchString) || x.LastName.Contains(searchString)
                || x.PersonalNumber.Contains(searchString) || x.DateOfBirth.ToString().Contains(searchString));

                return View(filteredModel);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        public IActionResult StudentDetails(int id)
        {
            Student student = _context.Students.FirstOrDefault(s => s.Id == id && !s.IsDeleted);
            if(student == null)
            {
                throw new Exception("");
            }

            StudentDetailsViewModel model = new()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email= student.Email,
                DateOfBirth= student.DateOfBirth,
                PersonalNumber = student.PersonalNumber

            };

            return View(model);
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

        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            Student student = _context.Students.Find(id);
            EditStudentViewModel editStudent = new()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PersonalNumber= student.PersonalNumber,
                DateOfBirth= student.DateOfBirth,
            };

            return View(editStudent);
        }

        [HttpPost]
        public IActionResult EditStudent(EditStudentViewModel model)
        {
            Student student = _context.Students.Find(model.Id);
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.Email = model.Email;
            student.PersonalNumber = model.PersonalNumber;
            student.DateOfBirth = model.DateOfBirth;

            _context.SaveChanges();

            return RedirectToAction("StudentDetails", new { id = student.Id });
        }

        [HttpPost]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            student.IsDeleted = true;

            _context.SaveChanges();

            return RedirectToAction("StudentIndex");
        }
    }
}
