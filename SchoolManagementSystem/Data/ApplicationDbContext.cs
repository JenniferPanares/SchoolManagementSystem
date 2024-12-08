using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Create a password hasher
        var passwordHasher = new PasswordHasher<IdentityUser>();

        // Sample password to hash
        string password = "Test@123";

        // Seed data for the Student table
        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Address = "123 Main Street",
                DateOfBirth = new DateTime(2000, 1, 1),
                PasswordHash = passwordHasher.HashPassword(null, password), // Hash the password
                Courses = new List<string> { "Math 101", "Science 101" }
            },
            new Student
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Address = "456 Elm Street",
                DateOfBirth = new DateTime(1998, 5, 15),
                PasswordHash = passwordHasher.HashPassword(null, password), // Hash the password
                Courses = new List<string> { "English 101", "History 101" }
            }
        );
    }
}
