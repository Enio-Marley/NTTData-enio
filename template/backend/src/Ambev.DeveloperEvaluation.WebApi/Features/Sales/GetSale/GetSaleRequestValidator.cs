using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleRequestValidator : AbstractValidator<GetSaleRequest>
    {
        /// <summary>
        /// Initializes validation rules for GetSaleRequest
        /// </summary>
        public GetSaleRequestValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty()
                .WithMessage("Sale number is required");
        }
    }
}
