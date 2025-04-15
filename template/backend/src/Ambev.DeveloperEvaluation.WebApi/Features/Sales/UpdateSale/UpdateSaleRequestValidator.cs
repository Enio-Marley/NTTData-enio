using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Validator for UpdateSaleRequest that defines validation rules for sale update.
    /// </summary>
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Must not be empty
        /// - BranchId: Must not be empty
        /// - BranchName: Required, must be between 3 and 150 characters
        /// - SaleNumber: Required, must not be empty
        /// - CustomerId: Must not be empty
        /// - CustomerName: Required, must be between 3 and 150 characters
        /// </remarks>
        public UpdateSaleRequestValidator()
        {
            RuleFor(sale => sale.Id).NotEmpty();
            RuleFor(sale => sale.BranchId).NotEmpty();
            RuleFor(sale => sale.BranchName).NotEmpty().Length(3, 150);
            RuleFor(sale => sale.SaleNumber).NotEmpty().GreaterThan(0);
            RuleFor(sale => sale.CustomerId).NotEmpty();
            RuleFor(sale => sale.CustomerName).NotEmpty().Length(3, 150);
            RuleForEach(item => item.Items).SetValidator(new UpdateSaleItemWebValidator());
        }
    }

    public class UpdateSaleItemWebValidator : AbstractValidator<SaleItemRequest>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateSaleIteWebValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Must not be empty
        /// - ProductId: Must not be empty
        /// - ProductName: Required, must be between 3 and 150 characters
        /// - Quantity:  must be greater than 0 and less than or equal to 20
        /// - UnitPrice: must be greater than or equal zero 
        /// </remarks>
        public UpdateSaleItemWebValidator()
        {
            RuleFor(sale => sale.ProductId).NotEmpty();
            RuleFor(sale => sale.ProductName).NotEmpty().Length(3, 150);
            RuleFor(sale => sale.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
            RuleFor(sale => sale.UnitPrice).GreaterThanOrEqualTo(0);
        }
    }
}
