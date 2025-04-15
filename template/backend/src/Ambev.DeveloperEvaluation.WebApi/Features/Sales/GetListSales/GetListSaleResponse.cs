namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetListSales
{
    public class GetListSaleResponse
    {
        /// <summary>
        /// The unique identifier of the sale
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
        /// Gets the list of items included in the sale.
        /// </summary>
        public List<SaleItemResponse> Items { get; set; } = new();

        /// <summary>
        /// Indicates whether the sale has been cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets the total amount of the sale
        /// </summary>
        public decimal TotalAmount => Items?.Sum(i => i.TotalAmount) ?? 0;

    }

    public class SaleItemResponse
    {
        /// <summary>
        /// The unique identifier of the sale item
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

        /// <summary>
        /// Gets the discount applied based on the quantity.
        //// </summary>
        public decimal Discount => CalculateDiscount(Quantity, UnitPrice);


        /// <summary>
        /// Gets the total amount for this item, considering unit price and discount.
        /// </summary>
        public decimal TotalAmount => (UnitPrice * Quantity) - Discount;

        private decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity >= 10 && quantity <= 20)
                return unitPrice * quantity * 0.20m;
            if (quantity >= 4)
                return unitPrice * quantity * 0.10m;

            return 0;
        }
    }
}
