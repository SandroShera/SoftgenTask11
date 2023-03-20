namespace SoftgenTask11.Models
{
    public class AddStudentToGroup
    {
        public int groupId { get; set; }
        public int studentId { get; set; }

        public IEnumerable<Student> Students { get; set;}
    }
}
