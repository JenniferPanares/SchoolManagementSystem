using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;


public class CoursesController : Controller
{
    private readonly ApplicationDbContext _context;

    public CoursesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Courses
    public IActionResult Index()
    {
        var courses = _context.Courses.ToList();
        return View(courses);
    }

    // GET: Courses/Details/5
    public IActionResult Details(int id)
    {
        var course = _context.Courses.FirstOrDefault(c => c.CourseId == id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    // GET: Courses/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Courses/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Course course)
    {
        if (ModelState.IsValid)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    // GET: Courses/Edit/5
    public IActionResult Edit(int id)
    {
        var course = _context.Courses.FirstOrDefault(c => c.CourseId == id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    // POST: Courses/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Course course)
    {
        if (id != course.CourseId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    // GET: Courses/Delete/5
    public IActionResult Delete(int id)
    {
        var course = _context.Courses.FirstOrDefault(c => c.CourseId == id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    // POST: Courses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var course = _context.Courses.FirstOrDefault(c => c.CourseId == id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    // POST: Courses/Enroll
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Enroll(int courseId, int studentId)
    {
        var course = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);
        var student = _context.Students.FirstOrDefault(s => s.StudentId == studentId);

        if (course == null || student == null)
        {
            return NotFound("Course or student not found.");
        }

        var enrollment = new Enrollment
        {
            CourseId = courseId,
            StudentId = studentId
        };

        _context.Enrollments.Add(enrollment);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
