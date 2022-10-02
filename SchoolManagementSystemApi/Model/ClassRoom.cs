using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemApi.Model
{
    public class ClassRoom: BaseClass
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public string ShortCode { get; set; }
        
    }
}
