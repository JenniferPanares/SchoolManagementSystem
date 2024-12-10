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
                var hashedPassword = HashPassword(model.Password);

                var student = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PasswordHash = hashedPassword,
                    DateOfBirth = DateTime.Now // Placeholder; add proper DOB input in the form
                };

                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(model);
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


        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
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
