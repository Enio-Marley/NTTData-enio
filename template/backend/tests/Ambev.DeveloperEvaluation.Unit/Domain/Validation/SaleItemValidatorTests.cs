using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the SaleItemValidator class.
/// Tests cover validation of sale item properties including ProductName, Quantity and UnitPrice
/// </summary>
public class SaleItemValidatorTests
{
    private readonly SaleItemValidator _validator;

    public SaleItemValidatorTests()
    {
        _validator = new SaleItemValidator();
    }

    /// <summary>
    /// Tests that validation passes when all sale item properties are valid.
    /// This test verifies that a sale item with valid
    /// passes all validation rules without any errors.
    /// </summary>
    [Fact(DisplayName = "Valid sale item should pass all validation rules")]
    public void Given_ValidSaleItem_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var saleItem = SaleTestData.GenerateItemValidSale();

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails for invalid product name formats.
    /// This test verifies that product name that are:
    /// - Empty strings
    /// - Less than 3 characters
    /// fail validation with appropriate error messages.
    /// The product name is a required field and must be between 3 and 150 characters.
    /// </summary>
    /// <param name="productName">The invalid product name to test.</param>
    [Theory(DisplayName = "Invalid product name formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public void Given_InvalidProductName_When_Validated_Then_ShouldHaveError(string productName)
    {
        // Arrange
        var saleItem = SaleTestData.GenerateItemValidSale();
        saleItem.ProductName = productName;

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductName);
    }

    /// <summary>
    /// Tests that validation fails when product name exceeds maximum length.
    /// This test verifies that product name longer than 150 characters fail validation.
    /// The test uses TestDataGenerator to create a product name that exceeds the maximum
    /// length limit, ensuring the validation rule is properly enforced.
    /// </summary>
    [Fact(DisplayName = "Product name longer than maximum length should fail validation")]
    public void Given_ProductNameLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var saleItem = SaleTestData.GenerateItemValidSale();
        saleItem.ProductName = SaleTestData.GenerateLongName();
        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductName);
    }

    /// <summary>
    /// Validated when a sale item exceeds the permitted quantity limit
    /// </summary>
    [Fact(DisplayName = "Sale Item should be valid when all item quantities are within limits")]
    public void Given_SaleItem_Should_Validate_Item_Quantity_Limit()
    {
        // Arrange
        var saleItem = SaleTestData.GenerateSaleItemWithInvalidIQuantity();

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.IsValid.Should().Be(false);
    }

    /// <summary>
    /// Validated when a sale item unit price is negative
    /// </summary>
    [Fact(DisplayName = "Sale Item should be valid when unit price is negative")]
    public void Given_Should_Have_Error_When_UnitPrice_Is_Negative()
    {
        // Arrange
        var saleItem = SaleTestData.GenerateSaleItemWithInvalidUnitPrice();

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.UnitPrice);
    }

}
