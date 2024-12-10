namespace SchoolManagementSystem.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SchoolManagementSystem.Models;

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the primary key for Enrollment
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.EnrollmentId);

            // Configure the relationship between Enrollment and Student
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.SetNull); // Prevent cascade delete

            // Configure the relationship between Enrollment and Course
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.SetNull); // Prevent cascade delete

            // Create a password hasher
            var passwordHasher = new PasswordHasher<IdentityUser>();

            // Sample password to hash
            string password = "Test@123";

            // Seed data for the Student table
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Address = "123 Main Street",
                    DateOfBirth = new DateTime(2000, 1, 1),
                    PasswordHash = passwordHasher.HashPassword(null, password)
                },
                new Student
                {
                    StudentId = 2,
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
}