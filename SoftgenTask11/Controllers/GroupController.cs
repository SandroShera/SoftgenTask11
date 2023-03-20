using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftgenTask11.Data;
using SoftgenTask11.Models;
using SoftgenTask11.ViewModels;

namespace SoftgenTask11.Controllers
{
    public class GroupController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GroupIndex(string? searchString)
        {
            var model = _context.Groups.Where(i => !i.IsDeleted);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredModel = model.Where(m => m.Number.Contains(searchString));
                return View(filteredModel);
            }
            return View(model);
        }

        public IActionResult GroupDetails(int id)
        {
            
            Group group = _context.Groups.Include(e => e.Students).Include(e=> e.Lecturers).FirstOrDefault(i => i.Id == id && !i.IsDeleted);
            if(group == null)
            {
                throw new Exception("");
            }

            GroupDetailsViewModel viewModel = new()
            {
                GroupId= group.Id,
                Name = group.Name,
                Number = group.Number,
                Lecturers= group.Lecturers,
                Students = group.Students
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddGroup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGroup(GroupCreateViewModel model)
        {
            Group group = new()
            {
                Name = model.Name,
                Number = model.Number
            };

            _context.Groups.Add(group);
            _context.SaveChanges();

            return RedirectToAction("GroupIndex");
        }

        [HttpPost]
        public IActionResult RemoveStudent(GroupDetailsViewModel data)
        {
            var student = _context.Students.Find(data.StudentId);
            var group = _context.Groups.Find(data.GroupId);

            group.Students.Remove(student);
            _context.SaveChanges();

            return RedirectToAction("GroupDetails", new { id = group.Id });
        }

        [HttpPost]
        public IActionResult RemoveLecturer(GroupDetailsViewModel data)
        {
            var lecturer = _context.Lecturers.Find(data.LecturerId);
            var group = _context.Groups.Find(data.GroupId);

            _context.Database.ExecuteSqlInterpolated($"delete from GroupLecturer where GroupsId={group.Id} and LecturersId={lecturer.Id}");

            _context.SaveChanges();

            return RedirectToAction("GroupDetails", new { id = group.Id });
        }

        [HttpPost]
        public IActionResult DeleteGroup(int id)
        {
            var group = _context.Groups.Find(id);
            group.IsDeleted = true;

            _context.SaveChanges();

            return RedirectToAction("GroupIndex");
        }

        [HttpGet]
        public IActionResult AddStudent(int id) 
        {
            var students = _context.Students;
            StudentToGroup model = new()
            {
                groupId = id,
                Students = students
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddStudent(StudentToGroup model)
        {
            var group = _context.Groups.Find(model.groupId);
            var student = _context.Students.Find(model.studentId);
            
            group.Students.Add(student);

            _context.SaveChanges();

            return RedirectToAction("GroupDetails", new {id = group.Id});
        }

        [HttpGet]
        public IActionResult AddLecturer(int id)
        {
            var lecturers = _context.Lecturers.Include(l => l.Groups);

            LecturerToGroup model = new()
            {
                GroupId = id,
            };

            foreach (var lecturer in lecturers)
            {
                if(lecturer.Groups.Any(g => g.Id == id))
                {
                    continue;
                }
                else
                {
                    model.Lecturers.Add(lecturer);
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddLecturer(LecturerToGroup data)
        {
            var group = _context.Groups.Find(data.GroupId);
            var lecturer = _context.Lecturers.Find(data.LecturerId);
            group.Lecturers.Add(lecturer);

            _context.SaveChanges();

            return RedirectToAction("Groupdetails", new { id = group.Id });
        }

        [HttpGet]
        public IActionResult EditGroup(int id)
        {
            Group group = _context.Groups.Find(id);
            EditGroupViewModel editGroup = new()
            {
                Id = group.Id,
                Name = group.Name,
                Students = group.Students,
                Lecturers = group.Lecturers
            };

            return View(editGroup);
        }
        [HttpPost]
        public IActionResult EditGroup(EditGroupViewModel model)
        {
            Group group = _context.Groups.Find(model.Id);
            group.Name = model.Name;
            group.Number = model.Number;

            _context.SaveChanges();

            return RedirectToAction("GroupDetails", new { id = group.Id });
        }

    }
}
