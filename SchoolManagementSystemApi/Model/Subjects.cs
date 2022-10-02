using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemApi.Model
{
    public class Subjects
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string SubjectShortCode { get; set; }
    }
}
