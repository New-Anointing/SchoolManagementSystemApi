using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemApi.Model
{
    public class Organisation
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string? SchoolName { get; set; }
        [Required]
        public string? Address { get; set; }
    }
}
