using FluentValidation;
using SchoolManagementSystem.ViewModels;

namespace SchoolManagementSystem.Validation
{
    public class PaymentViewModelValidator : AbstractValidator<PaymentViewModel>
    {
        public PaymentViewModelValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("The payment amount must be greater than zero.")
                .NotEmpty().WithMessage("The payment amount is required.");
        }
    }
}
