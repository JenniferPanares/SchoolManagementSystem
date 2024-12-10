using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = HashPassword(model.Password); // Call the hashing method

                var student = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PasswordHash = hashedPassword,
                    DateOfBirth = model.DateOfBirth,
                };

                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Login"); // Redirect to login page after successful registration
            }

            // Log validation errors for debugging (optional)
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Validation Error: {error.ErrorMessage}");
            }

            return View(model); // Return the model back to the form if validation fails
        }

        // Helper method to hash passwords
        private string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<IdentityUser>();
            return passwordHasher.HashPassword(null, password);
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = _context.Students.SingleOrDefault(s => s.Email == model.Email);

                if (student != null && VerifyPassword(model.Password, student.PasswordHash))
                {
                    // Redirect to Student Dashboard after login
                    return RedirectToAction("Dashboard", "Student", new { id = student.StudentId });
                }

                ModelState.AddModelError("", "Invalid email or password");
            }

            return View(model);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }

        public IActionResult Dashboard(int id)
        {
            var student = _context.Students.SingleOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            return View(student);
        }



    }
}
