using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemApi.Model
{
    public class Subjects : BaseClass
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Subject { get; set; }
    }
}
