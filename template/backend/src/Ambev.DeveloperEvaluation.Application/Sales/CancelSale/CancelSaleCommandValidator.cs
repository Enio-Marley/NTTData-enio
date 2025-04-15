using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Validator for CancelSaleCommand
    /// </summary>
    public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
    {
        /// <summary>
        /// Initializes validation rules for GetSaleCommand
        /// </summary>
        public CancelSaleCommandValidator()
        {
            RuleFor(x => x.SaleNumber)
               .NotEmpty()
               .WithMessage("Sale number is required");
        }
    }
}
