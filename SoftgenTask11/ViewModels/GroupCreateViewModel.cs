using SoftgenTask11.Models;

namespace SoftgenTask11.ViewModels
{
    public class GroupCreateViewModel
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
