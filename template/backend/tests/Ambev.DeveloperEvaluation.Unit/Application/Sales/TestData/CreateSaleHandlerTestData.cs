using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public class CreateSaleHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid CreateSaleCommand.
        /// The generated sale will have valid:
        /// - BranchId
        /// - BranchName
        /// - CustomerId
        /// - CustomerName
        /// - SaleNumber
        /// </summary>
        private static readonly Faker<CreateSaleCommand> creatSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(u => u.BranchId, f => f.Random.Guid())
        .RuleFor(u => u.BranchName, f => f.Company.CompanyName())
        .RuleFor(u => u.CustomerId, f => f.Random.Guid())
        .RuleFor(u => u.CustomerName, f => f.Company.CompanyName())
        .RuleFor(u => u.SaleNumber, f => f.Random.Long(1,99999))
        .RuleFor(s => s.Items, f => SaleItemFaker.Generate(f.Random.Int(1, 5)));

        /// <summary>
        /// Configures the Faker to generate valid SaleItemCommand.
        /// The generated sales items will have valid:
        /// </summary>
        private static readonly Faker<SaleItemCommand> SaleItemFaker = new Faker<SaleItemCommand>()
            .RuleFor(u => u.ProductId, f => f.Random.Guid())
            .RuleFor(u => u.ProductName, f => f.Commerce.ProductName())
            .RuleFor(u => u.Quantity, f => f.Random.Int(1, 20))
            .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(5, 100));

        /// <summary>
        /// Generates a valid CreateSaleCommand with randomized data.
        /// The generated sale will have all properties populated with valid values
        /// that meet the system's validation requirements.
        /// </summary>
        /// <returns>A valid CreateSaleCommand with randomly generated data.</returns>
        public static CreateSaleCommand GenerateValidCommand()
        {
            return creatSaleHandlerFaker.Generate();
        }
    }
}
