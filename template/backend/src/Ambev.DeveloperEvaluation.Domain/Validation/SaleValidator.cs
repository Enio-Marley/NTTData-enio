using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(item => item.BranchId)
               .NotEmpty()
               .WithMessage("Branch ID cannot be empty.");

            RuleFor(item => item.BranchName)
                .NotEmpty()
                .WithMessage("Branch name cannot be empty.")
                .Length(3, 150)
                .WithMessage("Branch name must be between 3 and 150 characters.");

            RuleFor(item => item.CustomerId)
               .NotEmpty()
               .WithMessage("Customer ID cannot be empty.");

            RuleFor(item => item.CustomerName)
                .NotEmpty()
                .WithMessage("Customer name cannot be empty.")
                .Length(3, 150)
                .WithMessage("Customer name must be between 3 and 150 characters.");

            RuleFor(item => item.TotalAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total amount must be greater than zero.");

            RuleFor(item => item.SaleNumber)
                .NotEmpty()
                .WithMessage("Sale number cannot be empty.")
                .GreaterThan(0)
                .WithMessage("Sale number must be greater than zero.");

            RuleForEach(item => item.Items).SetValidator(new SaleItemValidator());
        }
    }
}
