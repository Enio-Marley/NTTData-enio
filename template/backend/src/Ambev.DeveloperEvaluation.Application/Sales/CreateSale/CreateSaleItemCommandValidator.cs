using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleItemCommandValidator : AbstractValidator<SaleItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleItemCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - ProductId: Must not be empty
        /// - ProductName: Required, must be between 3 and 150 characters
        /// - Quantity:  must be greater than 0 and less than or equal to 20
        /// - UnitPrice: must be greater than or equal zero 
        /// </remarks>
        public CreateSaleItemCommandValidator()
        {
            RuleFor(sale => sale.ProductId).NotEmpty();
            RuleFor(sale => sale.ProductName).NotEmpty().Length(3, 150);
            RuleFor(sale => sale.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
            RuleFor(sale => sale.UnitPrice).GreaterThanOrEqualTo(0);
        }
    }
}
