using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; } // Primary Key
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Credits { get; set; }

        // Navigation Properties
        public ICollection<Enrollment> Enrollments { get; set; }
    }

}
