using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemApi.Model
{
    public class OrganisationRegistration : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public string? SchoolName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Role { get; set; }
        [Required]
        public string OrgId { get; set; }

    }
}
