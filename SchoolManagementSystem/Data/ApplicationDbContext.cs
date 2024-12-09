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
                PasswordHash = passwordHasher.HashPassword(null, password)
            },
            new Student
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Address = "456 Elm Street",
                DateOfBirth = new DateTime(1998, 5, 15),
                PasswordHash = passwordHasher.HashPassword(null, password)
            }
        );

        // Seed data for the Courses table
        modelBuilder.Entity<Course>().HasData(
            new Course { CourseId = 1, CourseTitle = "Math 101", Description = "Basic Mathematics", Credits = 3 },
            new Course { CourseId = 2, CourseTitle = "Science 101", Description = "Introduction to Science", Credits = 4 },
            new Course { CourseId = 3, CourseTitle = "English 101", Description = "English Literature Basics", Credits = 3 },
            new Course { CourseId = 4, CourseTitle = "History 101", Description = "World History Overview", Credits = 2 }
        );
    }
}
