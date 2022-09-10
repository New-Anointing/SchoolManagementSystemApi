using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemApi.Model
{
    public class Organisation
    {
        [Key]
        public string Id { get; set; }
        public string OrganisationName { get; set; }
    }
}
