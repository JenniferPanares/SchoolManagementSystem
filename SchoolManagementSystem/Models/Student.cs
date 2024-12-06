namespace SchoolManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; } // Primary Key
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty; // Store hashed password
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
