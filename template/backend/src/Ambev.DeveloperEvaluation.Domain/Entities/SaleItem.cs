using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item in a sale transaction.
    /// Contains product details, quantity, unit price, calculated discount, and total amount.
    /// </summary>
    public sealed class SaleItem : BaseEntity
    {
        /// <summary>
        /// Gets the unique identifier of the related sale.
        /// </summary>
        public Guid SaleId { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the product.
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the quantity of the product in the sale.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Gets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Gets the discount applied based on the quantity.
        /// </summary>
        public decimal Discount { get; private set; }

        /// <summary>
        /// Gets the total amount for this item, considering unit price and discount.
        /// </summary>
        public decimal TotalAmount => (UnitPrice * Quantity) - Discount;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItem"/> class with product details and calculates discount.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <param name="productName">The name of the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        /// <param name="unitPrice">The unit price of the product.</param>
        public SaleItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = CalculateDiscount(quantity, unitPrice);
        }

        /// <summary>
        /// Calculates the discount based on the quantity of items purchased.
        /// </summary>
        /// <param name="quantity">The quantity of the product.</param>
        /// <param name="unitPrice">The unit price of the product.</param>
        /// <returns>The calculated discount.</returns>
        private decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity >= 10 && quantity <= 20)
                return unitPrice * quantity * 0.20m;
            if (quantity >= 4)
                return unitPrice * quantity * 0.10m;

            return 0;
        }

        /// <summary>
        /// Update the instance of the <see cref="SaleItem"/> class with product details and calculates discount.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <param name="productName">The name of the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        /// <param name="unitPrice">The unit price of the product.</param>
        public void Update(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = CalculateDiscount(quantity, unitPrice);
        }
    }
}
