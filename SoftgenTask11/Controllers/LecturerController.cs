using Microsoft.AspNetCore.Mvc;
using SoftgenTask11.Data;
using SoftgenTask11.Models;
using SoftgenTask11.ViewModels;

namespace SoftgenTask11.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult LecturerIndex(string searchString)
        {
            var model = _context.Lecturers.Where(l => !l.IsDeleted);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredModel = model.Where(x => x.FirstName.Contains(searchString) || x.LastName.Contains(searchString)
                || x.PersonalNumber.Contains(searchString) || x.DateOfBirth.ToString().Contains(searchString));

                return View(filteredModel);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddLecturer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLecturer(LecturerAddViewModel model)
        {
            Lecturer lecturer = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PersonalNumber = model.PersonalNumber,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth
            };

            _context.Lecturers.Add(lecturer);
            _context.SaveChanges();

            return RedirectToAction("LecturerIndex");
        }

        public IActionResult LecturerDetails(int id)
        {
            Lecturer lecturer = _context.Lecturers.FirstOrDefault(l => l.Id == id && !l.IsDeleted);
            if(lecturer == null)
            {
                throw new Exception("");
            }

            LecturerDetailsViewModel model = new()
            {
                FirstName = lecturer.FirstName,
                LastName = lecturer.LastName,
                PersonalNumber = lecturer.PersonalNumber,
                Email = lecturer.Email,
                DateOfBirth = lecturer.DateOfBirth
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult EditLecturer(int id)
        {
            Lecturer lecturer = _context.Lecturers.Find(id);
            EditLecturerViewModel model = new()
            {
                Id = lecturer.Id,
                FirstName = lecturer.FirstName,
                LastName = lecturer.LastName,
                PersonalNumber = lecturer.PersonalNumber,
                Email = lecturer.Email,
                DateOfBirth = lecturer.DateOfBirth
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditLecturer(EditLecturerViewModel model)
        {
            Lecturer lecturer = _context.Lecturers.Find(model.Id);
            lecturer.FirstName = model.FirstName;
            lecturer.LastName = model.LastName;
            lecturer.PersonalNumber = model.PersonalNumber;
            lecturer.Email = model.Email;
            lecturer.DateOfBirth = model.DateOfBirth;

            _context.SaveChanges();
            return RedirectToAction("LecturerDetails", new {id = lecturer.Id});
        }

        [HttpPost]
        public IActionResult DeleteLecturer(int id)
        {
            var lecturer = _context.Lecturers.Find(id);
            lecturer.IsDeleted = true;

            _context.SaveChanges();

            return RedirectToAction("LecturerIndex");
        }
    }
}
