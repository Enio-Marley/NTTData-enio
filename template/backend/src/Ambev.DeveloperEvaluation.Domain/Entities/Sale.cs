using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction with customer and branch information,
    /// including sale items, discount rules, and cancellation logic.
    /// </summary>
    public sealed class Sale : BaseEntity
    {
        /// <summary>
        /// Gets the unique sale number.
        /// </summary>
        public long SaleNumber { get; private set; }

        /// <summary>
        /// Gets the date and time the sale was created.
        /// </summary>
        public DateTime SaleDate { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the customer.
        /// </summary>
        public Guid CustomerId { get; private set; }

        /// <summary>
        /// Gets the name of the customer.
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the unique identifier of the branch where the sale occurred.
        /// </summary>
        public Guid BranchId { get; private set; }

        /// <summary>
        /// Gets the name of the branch where the sale occurred.
        /// </summary>
        public string BranchName { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the sale has been cancelled.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets the total amount of the sale
        /// </summary>
        public decimal TotalAmount => Items.Sum(i => i.TotalAmount);

        /// <summary>
        /// Gets the list of items included in the sale.
        /// </summary>
        public List<SaleItem> Items { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Sale"/> class with the current date.
        /// </summary>
        public Sale()
        {
            SaleDate = DateTime.Now;
        }


        /// <summary>
        /// Performs validation of the sale entity using the SaleValidator rules defined in <see cref="SaleValidator"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed
        /// - Errors: Collection of validation errors if any rules failed
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        /// <summary>
        /// Cancels the sale, marking it as inactive.
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;
        }
    }
}
