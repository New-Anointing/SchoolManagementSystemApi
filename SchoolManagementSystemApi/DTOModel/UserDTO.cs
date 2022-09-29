namespace SchoolManagementSystemApi.DTOModel
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string HomeAdddress { get; set; }
        public string PhoneNumber { get; set; }
        public Genders Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }

        public enum Roles
        {
            Admin, Teacher, Student, Parent
        }
        public enum Genders 
        {
            Male, Female
        }
    }
}
