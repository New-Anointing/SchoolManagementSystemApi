using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemApi.Model
{
    public class Teachers : BaseClass
    {
        public Guid Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public Guid ClassRoomID { get; set; }

        public List<Subjects> Subjects { get; set; } = new List<Subjects>();


    }
}
