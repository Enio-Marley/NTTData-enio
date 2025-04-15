using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Command for creating a update sale.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for updating a sale, 
    /// including branch name, customer name and itens. 
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="UpdateSaleResult"/>.
    /// 
    /// The data provided in this command is validated using the 
    /// <see cref="UpdateSaleValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
    /// populated and follow the required rules.
    /// </remarks>
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        /// <summary>
        /// Gets the unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the saleNumber of the sale to be updated.
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
        public List<UpdateSaleItemCommand> Items { get; set; } = new();

        /// <summary>
        /// Indicates whether the sale has been cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }
    }
    public class UpdateSaleItemCommand
    {
        /// <summary>
        /// Gets the unique identifier of the sale item.
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
