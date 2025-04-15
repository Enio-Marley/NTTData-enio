using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command for creating a new sale.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for creating a sale, 
    /// including branch name, customer name, sale number and itens. 
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="CreateSaleResult"/>.
    /// 
    /// The data provided in this command is validated using the 
    /// <see cref="CreateSaleValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
    /// populated and follow the required rules.
    /// </remarks>
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
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
        public List<SaleItemCommand> Items { get; set; } = new();
    }
    public class SaleItemCommand
    {
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
