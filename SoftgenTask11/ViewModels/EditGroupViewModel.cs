using SoftgenTask11.Models;

namespace SoftgenTask11.ViewModels
{
    public class EditGroupViewModel
    {
        public EditGroupViewModel()
        {
            Students = new HashSet<Student>();
            Lecturers = new HashSet<Lecturer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public string Number { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
    }
}
