using FluentValidation;
using SchoolManagementSystem.Models;

public class EnrollmentValidator : AbstractValidator<Enrollment>
{
    public EnrollmentValidator()
    {
        RuleFor(enrollment => enrollment.StudentId)
            .GreaterThan(0).WithMessage("Student ID is required.");

        RuleFor(enrollment => enrollment.CourseId)
            .GreaterThan(0).WithMessage("Course ID is required.");

        RuleFor(enrollment => enrollment.EnrollmentDate)
            .NotEmpty().WithMessage("Enrollment date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Enrollment date cannot be in the future.");
    }
}
