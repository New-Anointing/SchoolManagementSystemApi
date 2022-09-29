﻿namespace SchoolManagementSystemApi.Model
{
    public class Teachers
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? SchoolId { get; set; }
        public string salary { get; set; }

    }
}
