using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }

}
