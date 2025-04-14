using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the SaleValidator class.
/// Tests cover validation of sale properties including BranchName, CustomerName and
/// SaleNumber requirements.
/// </summary>
public class SaleValidatorTests
{
    private readonly SaleValidator _validator;

    public SaleValidatorTests()
    {
        _validator = new SaleValidator();
    }

    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// This test verifies that a sale with valid
    /// passes all validation rules without any errors.
    /// </summary>

    [Fact(DisplayName = "Valid sale should pass all validation rules")]
    public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var user = SaleTestData.GenerateValidSale();

        // Act
        var result = _validator.TestValidate(user);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails for invalid branch name formats.
    /// This test verifies that branch name that are:
    /// - Empty strings
    /// - Less than 3 characters
    /// fail validation with appropriate error messages.
    /// The branch name is a required field and must be between 3 and 150 characters.
    /// </summary>
    /// <param name="branchName">The invalid branchName to test.</param>
    [Theory(DisplayName = "Invalid BranchName formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public void Given_InvalidBranchName_When_Validated_Then_ShouldHaveError(string branchName)
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.BranchName = branchName;

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BranchName);
    }

    /// <summary>
    /// Tests that validation fails when branch name exceeds maximum length.
    /// This test verifies that branch name longer than 150 characters fail validation.
    /// The test uses TestDataGenerator to create a branch name that exceeds the maximum
    /// length limit, ensuring the validation rule is properly enforced.
    /// </summary>
    [Fact(DisplayName = "Branch name longer than maximum length should fail validation")]
    public void Given_BranchLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.BranchName = SaleTestData.GenerateLongName();
        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BranchName);
    }

    /// <summary>
    /// Tests that validation fails when Customer name exceeds maximum length.
    /// This test verifies that Customer name longer than 150 characters fail validation.
    /// The test uses TestDataGenerator to create a Customer name that exceeds the maximum
    /// length limit, ensuring the validation rule is properly enforced.
    /// </summary>
    [Fact(DisplayName = "Customer name longer than maximum length should fail validation")]
    public void Given_CustomerLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.CustomerName = SaleTestData.GenerateLongName();
        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerName);
    }

    /// <summary>
    /// Tests that validation fails for invalid customer formats.
    /// This test verifies that customer name that are:
    /// - Empty strings
    /// - Less than 3 characters
    /// fail validation with appropriate error messages.
    /// The customer name is a required field and must be between 3 and 150 characters.
    /// </summary>
    /// <param name="customerName">The invalid customerName to test.</param>
    [Theory(DisplayName = "Invalid Customer Name formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public void Given_InvalidCustomerName_When_Validated_Then_ShouldHaveError(string customerName)
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.CustomerName = customerName;

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerName);
    }

    [Theory(DisplayName = "SaleNumber must be greater than zero")]
    [InlineData(0, false, "Sale number must be greater than zero.")]
    [InlineData(-1, false, "Sale number must be greater than zero.")]
    [InlineData(100, true, "")]
    public void Given_SaleNumber_Should_Be_Validated_According_To_Rules(long saleNumber, bool expectedValid, string expectedError)
    {
       // Arrange
       var sale = SaleTestData.GenerateValidSale();
        SaleTestData.SetRequiredProperties(sale, "Paul McCartney", "Let it Be", saleNumber);

        var validator = new SaleValidator();

       // Act
       var result = validator.Validate(sale);

       // Assert
        result.IsValid.Should().Be(expectedValid);
        if (!expectedValid)
        {
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedError);
        }
    }

}
