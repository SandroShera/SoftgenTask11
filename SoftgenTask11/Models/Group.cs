namespace SoftgenTask11.Models
{
    public class Group
    {
        public Group()
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
