using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemApi.Model
{
    public class TimeTable : BaseClass
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public virtual ClassRoom ClassRoom { get; set; }
        [Required]
        public virtual Subjects Subjects { get; set; }
        [Required]
        public DateTime TimeSchedule { get; set; }
    }
}
