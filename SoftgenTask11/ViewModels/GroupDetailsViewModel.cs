using SoftgenTask11.Models;

namespace SoftgenTask11.ViewModels
{
    public class GroupDetailsViewModel
    {
        public int? GroupId { get; set; }
        public int? StudentId { get; set; }
        public int? LecturerId { get; set; } 
        public string Name { get; set; }

        public string Number { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
    }
}
