using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemApi.Model
{
    public class Teachers : BaseClass
    {
        public Guid Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ClassRoom ClassRoom { get; set; }

        public virtual IEnumerable<Subjects> Subjects { get; set; }


    }
}
