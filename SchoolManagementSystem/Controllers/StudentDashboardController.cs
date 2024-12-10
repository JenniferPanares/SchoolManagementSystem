using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System.Linq;

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
        var email = User?.Identity?.Name;
        if (email == null)
        {
            return Unauthorized("User is not authenticated.");
        }

        var student = _context.Students
            .Include(s => s.Courses) // Ensure Courses are included
            .FirstOrDefault(s => s.Email == email);

        if (student == null)
        {
            return NotFound("Student not found.");
        }

        return View(student); // Student's dashboard view
    }

    // POST: AddCourse
    [HttpPost]
    public IActionResult AddCourse(string courseName)
    {
        var email = User?.Identity?.Name;
        if (email == null)
        {
            return Unauthorized("User is not authenticated.");
        }

        var student = _context.Students.FirstOrDefault(s => s.Email == email);

        if (student != null && !string.IsNullOrEmpty(courseName))
        {
            // Create a new Course object
            var newCourse = new Course
            {
                CourseTitle = courseName,
                Description = "Default description", // Or get this dynamically
                Credits = 3 // Default credits, modify as needed
            };

            student.Courses.Add(newCourse);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    // GET: Edit Profile
    public IActionResult Edit()
    {
        var email = User?.Identity?.Name;
        if (email == null)
        {
            return Unauthorized("User is not authenticated.");
        }

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
