using System.ComponentModel;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale to be created.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the saleNumber of the sale to be created.
        /// </summary>
        public long SaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the customer Id for the sale.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer name for the sale.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the branch id for the sale.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets or sets the branch name for the sale.
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// Gets or sets the branch name for the sale.
        /// </summary>
        [DefaultValue(false)]
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets the list of items included in the sale.
        /// </summary>
        public List<SaleItemRequest> Items { get; set; } = new();

    }
    public class SaleItemRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the unique identifier of the product.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the item sale.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the item sale.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
