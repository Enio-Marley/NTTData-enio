using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    /// <summary>
    /// Contains unit tests for the Sale entity class.
    /// Tests cover status changes and validation scenarios.
    /// </summary>
    public class SaleTests
    {
        /// <summary>
        /// Validates that a sale with 5 items receives a 10% discount 
        /// and calculates the total amount correctly.
        /// </summary>
        [Fact(DisplayName = "Sale with 10% discount should be valid and calculate total correctly")]
        public void Given_Sale_With_10Percent_Discount_Should_Be_Valid_And_Calculate_Total()
        {
            // Arrange
            var sale = SaleTestData.CreateValidSaleWithTenPercentDiscount();

            // Act
            var result = sale.Validate();

            // Assert
            result.IsValid.Should().BeTrue();
            sale.TotalAmount.Should().Be(45m);
        }

        /// <summary>
        /// Validates that a sale with 20% discount is correctly applied 
        /// and the total amount is calculated accordingly.
        /// </summary>
        [Fact(DisplayName = "Sale with 20% discount should be valid and calculate total correctly")]
        public void Given_Sale_With_20Percent_Discount_Should_Be_Valid_And_Calculate_Total()
        {
            // Arrange
            var sale = SaleTestData.CreateValidSaleWithTwentyPercentDiscount();

            // Act
            var result = sale.Validate();

            // Assert
            result.IsValid.Should().BeTrue();
            sale.TotalAmount.Should().Be(80m);
        }

        /// <summary>
        /// Tests whether the correct discount is applied based on item quantity.
        /// Uses inline data to verify scenarios for 0%, 10%, and 20% discounts.
        /// </summary>
        /// <param name="quantity">Quantity of items.</param>
        /// <param name="unitPrice">Price per unit.</param>
        /// <param name="expectedDiscount">Expected discount based on business rule.</param>
        [Theory(DisplayName = "SaleItem should apply correct discount based on quantity")]
        [InlineData(2, 10.0, 0.0)]         // sem desconto
        [InlineData(5, 10.0, 5.0)]         // 10% de 50 = 5
        [InlineData(10, 10.0, 20.0)]       // 20% de 100 = 20
        [InlineData(20, 5.0, 20.0)]        // 20% de 100 = 20
        public void Given_SaleItem_Should_Apply_Correct_Discount(int quantity, decimal unitPrice, decimal expectedDiscount)
        {
            // Arrange
            var item = new SaleItem(Guid.NewGuid(), "Produto Teste", quantity, unitPrice);

            // Act & Assert
            item.Discount.Should().Be(expectedDiscount);
        }

        /// <summary>
        /// Validated when a sale exceeds the permitted quantity limit
        /// </summary>
        [Fact(DisplayName = "Sale should be valid when all item quantities are within limits")]
        public void Given_Sale_Should_Validate_Item_Quantity_Limit()
        {
            // Arrange
            var sale = SaleTestData.GenerateSaleWithInvalidItemQuantity();

            // Act
            var result = sale.Validate();

            // Assert
            result.IsValid.Should().Be(false);
        }
    }
}
