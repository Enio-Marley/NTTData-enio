using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Initializes a delete instance of the DeleteSaleCommandValidator with defined validation rule.
    /// </summary>
    /// <remarks>
    /// Validation rule include:Id
    public class DeleteSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
    {
        public DeleteSaleCommandValidator()
        {
            RuleFor(sale => sale.Id).NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
