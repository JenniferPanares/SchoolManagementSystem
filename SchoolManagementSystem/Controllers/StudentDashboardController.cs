using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;

public class StudentDashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentDashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Dashboard
    public IActionResult Index()
    {
        var email = User.Identity.Name;
        var student = _context.Students.FirstOrDefault(s => s.Email == email);

        if (student == null)
        {
            return NotFound("Student not found.");
        }

        return View(student); // Student's dashboard view
    }

    // POST: AddCourse
    public IActionResult AddCourse(string courseName)
    {
        var email = User.Identity.Name;
        var student = _context.Students
            .Include(s => s.Courses) // Load related Courses
            .FirstOrDefault(s => s.Email == email);

        if (student != null && !string.IsNullOrEmpty(courseName))
        {
            // Ensure the student has a Courses collection
            if (student.Courses == null)
            {
                student.Courses = new List<Course>();
            }

            // Create a new Course object
            var newCourse = new Course
            {
                CourseTitle = courseName, // Set the course name
                StudentId = student.StudentId    // Associate the course with the student
            };

            // Add the Course object to the Courses collection
            student.Courses.Add(newCourse);

            // Save changes to the database
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    // GET: Edit Profile
    public IActionResult Edit()
    {
        var email = User.Identity.Name;
        var student = _context.Students.FirstOrDefault(s => s.Email == email);

        if (student == null)
        {
            return NotFound("Student not found.");
        }

        return View(student); // Edit profile view
    }

    // POST: Edit Profile
    [HttpPost]
    public IActionResult Edit(Student model)
    {
        if (ModelState.IsValid)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == model.StudentId);
            if (student != null)
            {
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.Email = model.Email;
                student.DateOfBirth = model.DateOfBirth;
                student.Address = model.Address;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        return View(model);
    }
}
