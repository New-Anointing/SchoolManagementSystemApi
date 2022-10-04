using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemApi.Model
{
    public class TimeTable : BaseClass
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid ClassId { get; set; }
        [ForeignKey("ClassId")]
        public virtual ClassRoom ClassRoom { get; set; }
        [Required]
        public Guid SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subjects Subjects { get; set; }
        [Required]
        public DateTime TimeSchedule { get; set; }
    }
}
