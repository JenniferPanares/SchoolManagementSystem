using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        // Additional fields specific to admins or teachers
        public string Department { get; set; } = string.Empty;
    }
}

