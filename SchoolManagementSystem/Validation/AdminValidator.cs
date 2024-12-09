using FluentValidation;
using SchoolManagementSystem.Models;

public class AdminValidator : AbstractValidator<Admin>
{
    public AdminValidator()
    {
        RuleFor(admin => admin.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(admin => admin.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(admin => admin.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

        RuleFor(admin => admin.Department)
            .MaximumLength(50).WithMessage("Department cannot exceed 50 characters.");
    }
}
