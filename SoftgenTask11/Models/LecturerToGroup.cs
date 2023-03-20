namespace SoftgenTask11.Models
{
    public class LecturerToGroup
    {
        public LecturerToGroup()
        {
            Lecturers = new();
        }
        public int? GroupId { get; set; }
        public int? LecturerId { get; set; }

        public List<Lecturer> Lecturers { get; set; }
    }
}
