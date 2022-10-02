namespace SchoolManagementSystemApi.DTOModel
{
    public class AdminUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string HomeAddress { get; set; }
        public string SchoolAddress { get; set; }
        public string PhoneNumber  { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public string? SchoolName { get; set; }

    }
}
