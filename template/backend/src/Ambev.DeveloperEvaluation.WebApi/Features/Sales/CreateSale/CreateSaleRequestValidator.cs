using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Validator for CreateSaleRequest that defines validation rules for sale creation.
    /// </summary>
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - BranchId: Must be empty
        /// - BranchName: Required, must be between 3 and 150 characters
        /// - SaleNumber: Required, must be empty
        /// - CustomerId: Must be empty
        /// - CustomerName: Required, must be between 3 and 150 characters
        /// - Items: Must not be null
        /// </remarks>
        public CreateSaleRequestValidator()
        {
            RuleFor(sale => sale.BranchId).NotEmpty();
            RuleFor(sale => sale.BranchName).NotEmpty().Length(3, 150);
            RuleFor(sale => sale.SaleNumber).NotEmpty().GreaterThan(0);
            RuleFor(sale => sale.CustomerId).NotEmpty();
            RuleFor(sale => sale.CustomerName).NotEmpty().Length(3, 150);
            RuleForEach(sale => sale.Items).Must(item => item != null)
                .WithMessage("Item cannot be null.");
        }
    }
}
