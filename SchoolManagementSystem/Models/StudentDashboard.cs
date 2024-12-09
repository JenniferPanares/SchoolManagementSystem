namespace SchoolManagementSystem.Models
{
    public class StudentDashboard
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Program { get; set; }
        public List<string> Courses { get; set; } = new List<string>(); // List of courses the student is enrolled in
    }
}
