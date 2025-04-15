using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - BranchId: Must not be empty
        /// - BranchName: Required, must be between 3 and 150 characters
        /// - SaleNumber: Required, must not be empty
        /// - CustomerId: Must not be empty
        /// - CustomerName: Required, must be between 3 and 150 characters
        /// - Items: Must not be null
        /// </remarks>
        public CreateSaleCommandValidator()
        {
            RuleFor(sale => sale.BranchId).NotEmpty();
            RuleFor(sale => sale.BranchName).NotEmpty().Length(3, 150);
            RuleFor(sale => sale.CustomerId).NotEmpty();
            RuleFor(sale => sale.CustomerName).NotEmpty().Length(3, 150);
            RuleForEach(sale => sale.Items).Must(item => item != null)
                .WithMessage("Item cannot be null.");
            RuleForEach(item => item.Items).SetValidator(new CreateSaleItemCommandValidator());
        }
    }
}
