using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleTestData
{

    /// <summary>
    /// Configures the Faker to generate valid SaleItem entities.
    /// The generated sales items will have valid:
    /// </summary>
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .CustomInstantiator(f =>
        {
            var productId = f.Random.Guid();
            var productName = f.Commerce.ProductName();
            var quantity = f.Random.Int(1, 20);
            var unitPrice = f.Random.Decimal(5, 100);
            return new SaleItem(productId, productName, quantity, unitPrice);
        });

    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated sales will have valid:
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(u => u.BranchId, f => f.Random.Guid())
        .RuleFor(u => u.BranchName, f => f.Company.CompanyName())
        .RuleFor(s => s.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.CustomerName, f => f.Name.FullName())
        .RuleFor(s => s.SaleNumber, f => f.Random.Long(1000, 9999))
        .RuleFor(s => s.SaleDate, f => f.Date.Past(1))
        .RuleFor(s => s.IsCancelled, f => f.Random.Bool())
        .RuleFor(s => s.Items, f => SaleItemFaker.Generate(f.Random.Int(1, 5)));

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated sale will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }

    /// <summary>
    /// Generates a valid SaleItem entity with randomized data.
    /// The generated sale item will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid SaleItem entity with randomly generated data.</returns>
    public static SaleItem GenerateItemValidSale()
    {
        return SaleItemFaker.Generate();
    }

    /// <summary>
    /// Generates a list of SaleItem instances with unit price set to zero,
    /// resulting in a total amount of zero — useful for testing validation and business rule failures.
    /// </summary>
    /// <param name="saleId">The identifier of the sale</param>
    /// <returns>A list of invalid</returns>
    public static List<SaleItem> GenerateInvalidItemSaleWithZeroTotalAmount(Guid saleId)
    {
        var itemFake = new Faker<SaleItem>()
            .CustomInstantiator(f =>
             {
                 var productId = f.Random.Guid();
                 var productName = f.Commerce.ProductName();
                 var quantity = 1;
                 var unitPrice = 0;
                 return new SaleItem(productId, productName, quantity, unitPrice);
             });

        return itemFake.Generate(2);
    }

    /// <summary>
    /// Creates a valid Sale entity that qualifies for a 10% discount
    /// by including a product with quantity between 4 and 9.
    /// </summary>
    /// <returns>A Sale entity applying a 10% discount rule.</returns>
    public static Sale CreateValidSaleWithTenPercentDiscount()
    {
        var sale = new Sale();
        SetRequiredProperties(sale, "Enio Marley", "Filial NTTDA", 12345);

        sale.Items.Add(new SaleItem(Guid.NewGuid(), "Guitarra Fender", 5, 10m));

        return sale;
    }

    /// <summary>
    /// Creates a valid Sale entity that qualifies for a 20% discount
    /// by including a product with quantity between 10 and 20.
    /// </summary>
    /// <returns>A Sale entity applying a 20% discount rule.</returns>
    public static Sale CreateValidSaleWithTwentyPercentDiscount()
    {
        var sale = new Sale();
        SetRequiredProperties(sale, "John Lennon", "Filial Beatles", 14562);

        sale.Items.Add(new SaleItem(Guid.NewGuid(), "Produto A", 10, 10m));

        return sale;
    }

    /// <summary>
    /// Generates a Sale entity with an invalid item
    /// that exceeds the maximum allowed quantity (greater than 20).
    /// </summary>
    /// <returns>A Sale entity containing an invalid item.</returns>
    public static Sale GenerateSaleWithInvalidItemQuantity()
    {
        var sale = new Sale();
        SetRequiredProperties(sale, "Ringo Star", "Filial Liverpool", 5678);

        sale.Items.Add(new SaleItem(Guid.NewGuid(), "Ticket to ride", 21, 15m));

        return sale;
    }

    /// <summary>
    /// Generates a SaleItem with quantity set above the allowed maximum (more than 20).
    /// </summary>
    /// <returns>An invalid SaleItem</returns>
    public static SaleItem GenerateSaleItemWithInvalidIQuantity()
    {
        return new SaleItem(Guid.NewGuid(), "Please Please Me", 21, 15m);
    }

    /// <summary>
    /// Generates a SaleItem with a negative unit price to test price validation logic.
    /// </summary>
    /// <returns>An invalid SaleItem with negative unit price.</returns>
    public static SaleItem GenerateSaleItemWithInvalidUnitPrice()
    {
        return new SaleItem(Guid.NewGuid(), "Abbey Road", 20, -1);
    }

    /// <summary>
    /// Generates a name that exceeds the maximum length limit.
    /// The generated name will:
    /// - Be longer than 150 characters
    /// - Contain random alphanumeric characters
    /// This is useful for testing name length validation error cases.
    /// </summary>
    /// <returns>A name that exceeds the maximum length limit.</returns>
    public static string GenerateLongName()
    {
        return new Faker().Random.String2(151);
    }

    /// <summary>
    /// Sets the required properties on a Sale instance using reflection,
    /// to facilitate test construction.
    /// </summary>
    /// <param name="sale">The Sale instance to modify.</param>
    /// <param name="customerName">The name of the customer to assign.</param>
    /// <param name="branchName">The name of the branch to assign.</param>
    /// <param name="saleNumber">The sale number to assign.</param>
    public static void SetRequiredProperties(Sale sale, string customerName, string branchName, long saleNumber)
    {
        typeof(Sale).GetProperty(nameof(Sale.CustomerId))!.SetValue(sale, Guid.NewGuid());
        typeof(Sale).GetProperty(nameof(Sale.CustomerName))!.SetValue(sale, customerName);
        typeof(Sale).GetProperty(nameof(Sale.BranchId))!.SetValue(sale, Guid.NewGuid());
        typeof(Sale).GetProperty(nameof(Sale.BranchName))!.SetValue(sale, branchName);
        typeof(Sale).GetProperty(nameof(Sale.SaleNumber))!.SetValue(sale, saleNumber);
    }
}
