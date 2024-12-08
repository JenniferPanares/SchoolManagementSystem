using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Course
    {
        public int CourseId { get; set; } // Primary Key
        public string CourseTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Credits { get; set; }

        // Navigation Properties
        public ICollection<Enrollment> Enrollments { get; set; }
    }

}
