using FluentValidation;
using SchoolManagementSystem.Models;

public class CourseValidator : AbstractValidator<Course>
{
    public CourseValidator()
    {
        RuleFor(course => course.CourseTitle)
            .NotEmpty().WithMessage("Course name is required.")
            .MaximumLength(100).WithMessage("Course name cannot exceed 100 characters.");

        RuleFor(course => course.StudentId)
            .GreaterThan(0).WithMessage("Student ID is required.");
    }
}
