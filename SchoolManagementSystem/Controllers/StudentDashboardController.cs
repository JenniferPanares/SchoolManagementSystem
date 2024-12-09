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
        var email = User.Identity.Name;
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
    public IActionResult AddCourse(int courseId)
    {
        var email = User.Identity.Name;
        var student = _context.Students
            .Include(s => s.Courses) // Ensure Courses are included
            .FirstOrDefault(s => s.Email == email);

        if (student != null)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);

            if (course != null && !student.Courses.Contains(course))
            {
                student.Courses.Add(course); // Add the course to the student
                _context.SaveChanges();
            }
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
            var student = _context.Students.FirstOrDefault(s => s.Id == model.Id);
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
