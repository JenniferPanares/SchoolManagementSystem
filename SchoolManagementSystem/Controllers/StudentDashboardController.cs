﻿using Microsoft.AspNetCore.Mvc;
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
    [HttpPost]
    public IActionResult AddCourse(string courseName)
    {
        var email = User.Identity.Name;
        var student = _context.Students.FirstOrDefault(s => s.Email == email);

        if (student != null && !string.IsNullOrEmpty(courseName))
        {
            // Ensure the student has a Courses list
            if (student.Courses == null)
            {
                student.Courses = new List<string>();
            }

            student.Courses.Add(courseName);
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
