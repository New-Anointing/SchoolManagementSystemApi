using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemApi.Model
{
    public class Events : BaseClass
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime EventDateTime { get; set; }
    }
}
