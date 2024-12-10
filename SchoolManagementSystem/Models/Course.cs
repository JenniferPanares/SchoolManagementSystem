using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Course
    {
        public int CourseId { get; set; } 

        [Required]
        [StringLength(100)]
        public string CourseTitle { get; set; } 

        [StringLength(500)]
        public string Description { get; set; } 

        [Range(1, 10)]
        public int Credits { get; set; } 

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int? AdminId { get; set; }
        public Admin Admin { get; set; }

        // Navigation property 
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
