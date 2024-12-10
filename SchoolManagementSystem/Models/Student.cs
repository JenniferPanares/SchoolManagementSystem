using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty; // Default value to avoid null
        public string LastName { get; set; } = string.Empty;  // Default value
        public string Email { get; set; } = string.Empty;     // Default value
        public string Address { get; set; } = string.Empty;   // Default value
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; } = string.Empty; // Default value

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}